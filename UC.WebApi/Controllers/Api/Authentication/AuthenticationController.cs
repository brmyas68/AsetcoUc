using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.Common.Enum;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;

namespace UC.WebApi.Controllers.Api.Authentication
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; readonly IConfiguration _Configuration; IMapper _IMapperlanguage;
        public AuthenticationController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperlanguage = MapperLanguage.MapTo(); }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string Mobile, [FromForm] string PassWord, [FromForm] string SecurityCode, [FromForm] string TokenSecurityCode)
        {
            if (Mobile == "" && PassWord == "" && SecurityCode == "" && SecurityCode.Length > 5 && Mobile.Length == 11) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var KeySecurity = _Configuration["Jwt:KeySecurityCode"].ToString();
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _TokenSecurityCode = KeySecurity + "Mehraman" + SecurityCode.ToString() + "AsetCo";
            string _HashTokenSecurityCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenSecurityCode);
            if (!(_UnitOfWorkUCService._ISecurityCodeService.IValidSecurityCode(TokenSecurityCode, _HashTokenSecurityCode))) return Ok(new { Message = MessageException.Messages.NotFoundSecurityCode.ToString(), Status = MessageException.Status.Status400 });
            if (!await _UnitOfWorkUCService._IUserService.CheckUserMobile(TenantID, Mobile)) return Ok(new { Message = MessageException.Messages.NotFoundMobile.ToString(), Status = MessageException.Status.Status400 });
            string PassHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(PassWord);
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_Mobile == Mobile && U.User_HashPassword == PassHash);
            if (_User == null) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            if (_User.User_IsBlock) return Ok(new { Message = MessageException.Messages.IsBlockUser.ToString(), Status = MessageException.Status.Status400 });
            var _IsSystemRole = await _UnitOfWorkUCService._IRoleService.IsSystemRole(_User.User_ID);
            if (!_IsSystemRole) return Ok(new { Message = MessageException.Messages.IsNotSystemRole.ToString(), Status = MessageException.Status.Status400 });
            var _KeyASE = _Configuration["Jwt:KeyASE"].ToString();
            var _TokenAse = _UnitOfWorkUCService._IAuthenticationService.EnCryptASE(_KeyASE, _User.User_ID.ToString());
            if (!_User.User_IsActive) return Ok(new { Token = _TokenAse, IsActive = _User.User_IsActive });
            DtoSettingToken _DtoSettingToken = new DtoSettingToken();
            _DtoSettingToken.Signing_Key = _Configuration["Jwt:Key"].ToString();
            _DtoSettingToken.Issuer = _Configuration["Jwt:Issuer"].ToString();
            _DtoSettingToken.Audience = _Configuration["Jwt:Audience"].ToString();
            _DtoSettingToken.Subject = _Configuration["Jwt:Subject"].ToString();
            _DtoSettingToken.Expir_Minutes = Convert.ToDouble(SettingToken.Expir.Minutes);
            _DtoSettingToken.DateTime_UtcNow = DateTime.UtcNow;
            _DtoSettingToken.DateTime_Now = DateTime.Now;
            _DtoSettingToken.Guid = Guid.NewGuid();
            var _Token = await _UnitOfWorkUCService._IAuthenticationService.CreateToken(_User.User_ID, _DtoSettingToken);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var Token = new Token();
            Token.Token_UserID = _User.User_ID;
            Token.Token_HashCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_Token);
            Token.Token_DateCreate = DateTime.Now;
            Token.Token_DateExpire = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours)); //1 Hours add
            Token.Token_DateLastAccessTime = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours));
            Token.Token_IsActive = true;
            await _UnitOfWorkUCService._ITokenService.Insert(Token);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Token = _Token, Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
        

        [AllowAnonymous]
        [Route("RefreshSecurityCode")]
        [HttpGet]
        public IActionResult RefreshSecurityCode()
        {
            int _SecurityCode = _UnitOfWorkUCService._ISecurityCodeService.GenarateSecurityCode();
            var KeySecurity = _Configuration["Jwt:KeySecurityCode"].ToString();
            string _TokenSecurityCode = KeySecurity + "Mehraman" + _SecurityCode.ToString() + "AsetCo";
            string _HashTokenSecurityCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenSecurityCode);
            var _Bitmap64 = _UnitOfWorkUCService._ISecurityCodeService.CreateImageSecurityCode(_SecurityCode);
            // await Task.CompletedTask;
            return Ok(new { BitmapSecurityCode = _Bitmap64, TokenSecurityCode = _HashTokenSecurityCode });
        }


        [AllowAnonymous]
        [Route("SendActiveCode")]
        [HttpPost]
        public async Task<IActionResult> SendActiveCode([FromForm] string ReceptorMobile)
        {
            if (ReceptorMobile == "") return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (!(await _UnitOfWorkUCService._IUserService.CheckUserMobile(TenantID, ReceptorMobile))) return Ok(new { Message = MessageException.Messages.NotFoundMobile.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService._ISmsService.Delete(ReceptorMobile))) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _ActiveCode = _UnitOfWorkUCService._ISmsService.GenerateActiveCode();
            var _TextMessage = "homacall"; var SenderMobile = "";
            if (_ActiveCode <= 0) return Ok(new { Message = MessageException.Messages.ErrorActiveCode.ToString(), Status = MessageException.Status.Status400 });
            var _Sms = await _UnitOfWorkUCService._ISmsService.Send(SenderMobile, ReceptorMobile, _ActiveCode.ToString(), _TextMessage);
            if (_Sms == null) return Ok(new { Message = MessageException.Messages.ErrorSendSms.ToString(), Status = MessageException.Status.Status400 });
            if (_Sms.Sms_Status == 400) return Ok(new { Message = MessageException.Messages.ErrorSendSms.ToString(), Status = MessageException.Status.Status400 });
            await _UnitOfWorkUCService._ISmsService.Insert(_Sms);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AllowAnonymous]
        [Route("IsValidActiveCode")]
        [HttpPost]
        public async Task<IActionResult> IsValidActiveCode([FromForm] string ReceptorMobile, [FromForm] string ActiveCode)
        {
            if (ReceptorMobile == "" && ActiveCode == "" && ActiveCode.Length > 5 && ReceptorMobile.Length > 11) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService._ISmsService.FindByActiveCode(ReceptorMobile, ActiveCode))) return Ok(new { Message = MessageException.Messages.NotFoundActiveCode.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_Mobile == ReceptorMobile);
            if (_User == null) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService._ISmsService.Delete(ReceptorMobile))) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var _KeyASE = _Configuration["Jwt:KeyASE"].ToString();
            var _Token = _UnitOfWorkUCService._IAuthenticationService.EnCryptASE(_KeyASE, _User.User_ID.ToString());
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Token = _Token, IsActive = false });
        }

        [AllowAnonymous]
        [Route("ActiveUser")]
        [HttpPost]
        public async Task<IActionResult> ActiveUser([FromForm] string TokenA, [FromForm] string NewPassWord, [FromForm] string ConfirmNewPassWord)
        {
            var _Token = string.Empty;
            if (TokenA == "" && NewPassWord == "" && NewPassWord.Length > 20 && ConfirmNewPassWord == "" && ConfirmNewPassWord.Length > 20) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            if (NewPassWord.CompareTo(ConfirmNewPassWord) != 0) return Ok(new { Message = MessageException.Messages.NotMatchPassWord.ToString(), Status = MessageException.Status.Status400 });
            var _KeyASE = _Configuration["Jwt:KeyASE"].ToString();
            string _UserID = _UnitOfWorkUCService._IAuthenticationService.DeCryptASE(_KeyASE, TokenA);
            var _NewPasswordHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(NewPassWord);
            if (!(await _UnitOfWorkUCService._IUserService.UpdatePassword(Convert.ToInt32(_UserID), _NewPasswordHash))) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            if (!(await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            DtoSettingToken _DtoSettingToken = new DtoSettingToken();
            _DtoSettingToken.Signing_Key = _Configuration["Jwt:Key"].ToString();
            _DtoSettingToken.Issuer = _Configuration["Jwt:Issuer"].ToString();
            _DtoSettingToken.Audience = _Configuration["Jwt:Audience"].ToString();
            _DtoSettingToken.Subject = _Configuration["Jwt:Subject"].ToString();
            _DtoSettingToken.Expir_Minutes = Convert.ToDouble(SettingToken.Expir.Minutes);
            _DtoSettingToken.DateTime_UtcNow = DateTime.UtcNow;
            _DtoSettingToken.DateTime_Now = DateTime.Now;
            _DtoSettingToken.Guid = Guid.NewGuid();
            var _TokenNew = await _UnitOfWorkUCService._IAuthenticationService.CreateToken(Convert.ToInt32(_UserID), _DtoSettingToken);
            if (_TokenNew == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var Token = new Token();
            Token.Token_UserID = Convert.ToInt32(_UserID);
            Token.Token_HashCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenNew);
            Token.Token_DateCreate = DateTime.Now;
            Token.Token_DateExpire = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours));
            Token.Token_DateLastAccessTime = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours));
            Token.Token_IsActive = true;
            await _UnitOfWorkUCService._ITokenService.Insert(Token);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Token = _TokenNew, Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}

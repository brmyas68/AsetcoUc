using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UC.ClassDTO.DTOs;
using UC.Common.Exceptions;
using UC.InterfaceService.InterfacesBase;
using UC.Common.Enum;
using UC.ClassDomain.Domains;

namespace UC.WebApi.Controllers.ApiGod.Authentication
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService;
        private readonly IConfiguration _Configuration;
        public AuthenticationGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] string UserName, [FromForm] string PassWord)
        {
            if (UserName == "" && PassWord == "") return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            string PassHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(PassWord);
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_UserName == UserName && U.User_HashPassword == PassHash);
            if (_User == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            if (!await _UnitOfWorkUCService._IAuthenticationService.IsValidAuthoriz(_User.User_ID, EnumPermission.Role.Role_UC_God.ToString())) return Ok(new { Message = MessageException.Messages.NotFoundRole.ToString(), Status = MessageException.Status.Status400 });
            DtoSettingToken _DtoSettingToken = new DtoSettingToken();
            _DtoSettingToken.Signing_Key = _Configuration["Jwt:Key"].ToString();
            _DtoSettingToken.Issuer = _Configuration["Jwt:Issuer"].ToString();
            _DtoSettingToken.Audience = _Configuration["Jwt:Audience"].ToString();
            _DtoSettingToken.Subject = _Configuration["Jwt:Subject"].ToString();
            _DtoSettingToken.Expir_Minutes = Convert.ToDouble(SettingToken.Expir.Minutes);  
            _DtoSettingToken.DateTime_UtcNow = DateTime.UtcNow;
            _DtoSettingToken.DateTime_Now = DateTime.Now;
            _DtoSettingToken.Guid = Guid.NewGuid();

            if (await _UnitOfWorkUCService._IAuthenticationService.CountUser(_User.User_ID) == 5) return Ok(new { Message = MessageException.Messages.UserCapacity.ToString(), Status = MessageException.Status.Status400 });
            var _Token = await _UnitOfWorkUCService._IAuthenticationService.CreateToken(_User.User_ID, _DtoSettingToken);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var Token = new Token();
            Token.Token_UserID = _User.User_ID;
            Token.Token_HashCode = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_Token);
            Token.Token_DateCreate = DateTime.Now;
            Token.Token_DateExpire = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours));
            Token.Token_DateLastAccessTime = DateTime.Now.AddHours(Convert.ToDouble(SettingToken.Expir.AddHours)); 
            Token.Token_IsActive = true;
            await _UnitOfWorkUCService._ITokenService.Insert(Token);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Token = _Token, Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}
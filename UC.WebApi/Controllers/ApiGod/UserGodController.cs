using Annex.ClassDomain.Domains;
using Annex.ClassDTO.DTOs.Customs;
using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.Common.Authorization;
using UC.Common.Enum;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;

namespace UC.WebApi.Controllers.ApiGod
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IUnitOfWorkAnnexService _UnitOfWorkAnnexService; readonly IConfiguration _Configuration; IMapper _IMapper;
        public UserGodController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperUser.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Users = await _UnitOfWorkUCService._IUserService.GetAll();
            if (Users == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Users = Users.Select(U => _IMapper.Map<User, DtoUser>(U)).ToList();
            return Ok(new { Users = _Users, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == ID);
            var _User = _IMapper.Map<User, DtoUser>(User);
            return Ok(new { User = _User, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllUserSP")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserSP()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "USRA");
            var _User = await _UnitOfWorkUCService._IUserService.GetAllUser_SP(_DtoAnnexSetting.AnnexSetting_ID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { User = _User, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoUser DtoUser, [FromForm] IFormFile IFileUser)
        {

            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (DtoUser == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            if ((await _UnitOfWorkUCService._IUserService.CheckUserName(TenantID, DtoUser.Usr_UName))) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            string _SerialNum = Guid.NewGuid().ToString("N");

            User _User = new User();
            _User.User_FirstName = DtoUser.Usr_FName;
            _User.User_LastName = DtoUser.Usr_LName;
            _User.User_IdentifyNumber = DtoUser.Usr_IdentNum;
            _User.User_Gender = DtoUser.Usr_Gender;
            _User.User_Email = DtoUser.Usr_mail;
            _User.User_Mobile = DtoUser.Usr_Mobile;
            _User.User_DateRegister = DateTime.Now;
            _User.User_UserName = DtoUser.Usr_UName;
            _User.User_HashPassword = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(DtoUser.Usr_HPass);
            _User.User_Province_ID = DtoUser.Usr_Prov_ID;
            _User.User_City_ID = DtoUser.Usr_Cty_ID;
            _User.User_Address = DtoUser.Usr_Address;
            _User.User_TenantID = 1;
            _User.User_IsActive = false;

            await _UnitOfWorkUCService._IUserService.Insert(_User);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
            {
                if (IFileUser.Length > 0)
                {
                    string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                    var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "USRA");
                    if (_DtoAnnexSetting == null) return Ok(new { Message = MessageException.Messages.NoFindPath.ToString(), Status = MessageException.Status.Status400 });
                    var _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + _User.User_ID;
                    var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                    if (!_boolDirectory) return Ok(new { Message = MessageException.Messages.NoDirectory.ToString(), Status = MessageException.Status.Status400 });
                    var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                    if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser, Status = MessageException.Status.Status400 });
                    Annexs _Annex = new Annexs()
                    {
                        Annex_Path = _Path.Replace("//", "/"),
                        Annex_FileNamePhysicy = _PhysicyNameFile,
                        Annex_FileNameLogic = IFileUser.FileName,
                        Annex_ReferenceID = _User.User_ID,
                        Annex_CreatedDate = DateTime.Now,
                        Annex_Description = "",
                        Annex_ReferenceFolderName = DtoUser.Usr_FName + " " + DtoUser.Usr_LName,
                        Annex_AnnexSettingID = _DtoAnnexSetting.AnnexSetting_ID,
                        Annex_FileExtension = Path.GetExtension(IFileUser.FileName),
                        Annex_IsDefault = true

                    };
                    await _UnitOfWorkAnnexService._IAnnexsService.Insert(_Annex);
                    await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                }

                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }

            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoUser DtoUser, [FromForm] IFormFile IFileUser)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (DtoUser == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == DtoUser.Usr_ID);
            if (_User != null)
            {
                _User.User_FirstName = DtoUser.Usr_FName;
                _User.User_LastName = DtoUser.Usr_LName;
                _User.User_IdentifyNumber = DtoUser.Usr_IdentNum;
                _User.User_Gender = DtoUser.Usr_Gender;
                _User.User_Email = DtoUser.Usr_mail;
                _User.User_Mobile = DtoUser.Usr_Mobile;
                if (DtoUser.Usr_UName != null)
                {
                    if ((await _UnitOfWorkUCService._IUserService.CheckUserName(TenantID, DtoUser.Usr_UName)))
                        return Ok(new { Message = MessageException.Messages.FoundUser.ToString(), Status = MessageException.Status.Status400 });
                    else
                        _User.User_UserName = DtoUser.Usr_UName;
                }
                if (DtoUser.Usr_HPass != "") _User.User_HashPassword = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(DtoUser.Usr_HPass);
                _User.User_Province_ID = DtoUser.Usr_Prov_ID;
                _User.User_City_ID = DtoUser.Usr_Cty_ID;
                _User.User_Address = DtoUser.Usr_Address;

                _UnitOfWorkUCService._IUserService.Update(_User);
                if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
                {
                    if (IFileUser.Length > 0)
                    {
                        var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "USRA");
                        var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
                        var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.User_ID && A.Annex_IsDefault == true);
                        var _Path = "";
                        if (_Annex != null)
                        {
                            string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                            _Path = _Annex.Annex_Path.Replace("/", "//");
                            var _PathExists = _Path + "//" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
                            if (await _UnitOfWorkAnnexService._IFtpService.Exists(_PathExists))
                            {
                                await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_PathExists);
                            }
                            var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                            if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                            _Annex.Annex_FileNameLogic = IFileUser.FileName;
                            _Annex.Annex_FileExtension = Path.GetExtension(IFileUser.FileName);
                            _Annex.Annex_FileNamePhysicy = _PhysicyNameFile;
                            _UnitOfWorkAnnexService._IAnnexsService.Update(_Annex);
                            await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                        }
                        else
                        {
                            string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                            _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + _User.User_ID;
                            var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                            var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                            if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                            Annexs Annex_ = new Annexs()
                            {
                                Annex_Path = _Path.Replace("//", "/"),
                                Annex_FileNamePhysicy = _PhysicyNameFile,
                                Annex_FileNameLogic = IFileUser.FileName,
                                Annex_ReferenceID = _User.User_ID,
                                Annex_CreatedDate = DateTime.Now,
                                Annex_Description = "",
                                Annex_ReferenceFolderName = _User.User_FirstName + " " + _User.User_LastName,
                                Annex_AnnexSettingID = _DtoAnnexSetting.AnnexSetting_ID,
                                Annex_FileExtension = Path.GetExtension(IFileUser.FileName),
                                Annex_IsDefault = true
                            };
                            await _UnitOfWorkAnnexService._IAnnexsService.Insert(Annex_);
                            await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                        }
                    }

                    return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
                }
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == ID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IUserService.Delete(_User);

            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
            {
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSettingUSRA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "USRA");
                await _UnitOfWorkAnnexService._IFtpService.DeleteDirectoryFiles(_AnnexSettingUSRA.AnnexSetting_Path + "//" + _User.User_ID + "//");
                var Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ReferenceID == _User.User_ID & A.Annex_AnnexSettingID == _AnnexSettingUSRA.AnnexSetting_ID);
                if (Annex != null)
                {
                    _UnitOfWorkAnnexService._IAnnexsService.Delete(Annex);
                    await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                }

                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("ISBlock")]
        [HttpPost]
        public async Task<IActionResult> ISBlock([FromForm] int UserID, [FromForm] bool ISBlock)
        {
            if (UserID < 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _State = await _UnitOfWorkUCService._IUserService.IsBlockUser(UserID, ISBlock);
            if (_State == false) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("ISActive")]
        [HttpPost]
        public async Task<IActionResult> ISActive([FromForm] int UserID, [FromForm] bool IsActive)
        {
            if (UserID < 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _State = await _UnitOfWorkUCService._IUserService.IsActiveUser(UserID, IsActive);
            if (_State == false) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("LogOut")]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            string _Token = string.Empty;
            if (this.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                _Token = this.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
                if (_Token != "")
                {
                    string[] _TokenMain = _Token.Split(" ");
                    string _TokenHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(_TokenMain[1]);
                    var Token = await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _TokenHash);
                    _UnitOfWorkUCService._ITokenService.Delete(Token);
                    if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
                }
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}

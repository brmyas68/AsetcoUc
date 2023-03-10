using Annex.ClassDomain.Domains;
using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.ClassDTO.DTOs.Filters;
using UC.Common.Authorization;
using UC.Common.Enum;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;

namespace UC.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IUnitOfWorkAnnexService _UnitOfWorkAnnexService; readonly IConfiguration _Configuration; IMapper _IMapperUser;
        public UserController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperUser = MapperUser.MapTo(); }


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int LangID, [FromForm] String? SearchText, [FromForm] DtoFilterUser? Filters, [FromForm] int PageIndex, [FromForm] int PageSize, [FromForm] string? SortItem, [FromForm] int SortType)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _Actions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_Actions(_UserID, TenantID, "Form_User");

            DataTable DtFilterUser = new DataTable();
            DtFilterUser.Columns.Add("TF_FieldName");
            DtFilterUser.Columns.Add("TF_Condition");
            DtFilterUser.Columns.Add("TF_Type");

            if (SearchText == null) SearchText = "";
            DtFilterUser.Rows.Add();
            DtFilterUser.Rows[0]["TF_FieldName"] = "SearchText";
            DtFilterUser.Rows[0]["TF_Condition"] = SearchText;
            DtFilterUser.Rows[0]["TF_Type"] = "0";

            if (PageIndex < 0) PageIndex = 0;
            DtFilterUser.Rows.Add();
            DtFilterUser.Rows[1]["TF_FieldName"] = "PageIndex";
            DtFilterUser.Rows[1]["TF_Condition"] = PageIndex;
            DtFilterUser.Rows[1]["TF_Type"] = "0";

            if (PageSize <= 0) PageSize = 50;
            DtFilterUser.Rows.Add();
            DtFilterUser.Rows[2]["TF_FieldName"] = "PageSize";
            DtFilterUser.Rows[2]["TF_Condition"] = PageSize;
            DtFilterUser.Rows[2]["TF_Type"] = "0";

            if (SortType < 0) SortType = 1;
            DtFilterUser.Rows.Add();
            DtFilterUser.Rows[3]["TF_FieldName"] = "SortType";
            DtFilterUser.Rows[3]["TF_Condition"] = SortType;
            DtFilterUser.Rows[3]["TF_Type"] = "0";

            if (SortItem == null) SortItem = "";
            DtFilterUser.Rows.Add();
            DtFilterUser.Rows[4]["TF_FieldName"] = "SortItem";
            DtFilterUser.Rows[4]["TF_Condition"] = SortItem;
            DtFilterUser.Rows[4]["TF_Type"] = "0";

            if (Filters != null)
            {
                if (Filters.Filter_Gender == null) Filters.Filter_Gender = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[5]["TF_FieldName"] = "User_Gender";
                DtFilterUser.Rows[5]["TF_Condition"] = Filters.Filter_Gender;
                DtFilterUser.Rows[5]["TF_Type"] = "0";

                if (Filters.Filter_City == null) Filters.Filter_City = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[6]["TF_FieldName"] = "User_City_ID";
                DtFilterUser.Rows[6]["TF_Condition"] = Filters.Filter_City;
                DtFilterUser.Rows[6]["TF_Type"] = "0";

                if (Filters.Filter_Province == null) Filters.Filter_Province = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[7]["TF_FieldName"] = "User_Province_ID";
                DtFilterUser.Rows[7]["TF_Condition"] = Filters.Filter_Province;
                DtFilterUser.Rows[7]["TF_Type"] = "0";

                if (Filters.Filter_DateStart == null) Filters.Filter_DateStart = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[8]["TF_FieldName"] = "User_DateRegister";
                DtFilterUser.Rows[8]["TF_Condition"] = Filters.Filter_DateStart;
                DtFilterUser.Rows[8]["TF_Type"] = "0";

                if (Filters.Filter_DateEnd == null) Filters.Filter_DateEnd = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[9]["TF_FieldName"] = "User_DateRegister1";
                DtFilterUser.Rows[9]["TF_Condition"] = Filters.Filter_DateEnd;
                DtFilterUser.Rows[9]["TF_Type"] = "0";

                if (Filters.Filter_UserState == null) Filters.Filter_UserState = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[10]["TF_FieldName"] = "User_IsActive";
                DtFilterUser.Rows[10]["TF_Condition"] = Filters.Filter_UserState;
                DtFilterUser.Rows[10]["TF_Type"] = "0";

                if (Filters.Filter_Roles == null) Filters.Filter_Roles = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[11]["TF_FieldName"] = "Roles_ID";
                DtFilterUser.Rows[11]["TF_Condition"] = Filters.Filter_Roles;
                DtFilterUser.Rows[11]["TF_Type"] = "0";

                if (Filters.Filter_SearchTargets == null) Filters.Filter_SearchTargets = "";
                DtFilterUser.Rows.Add();
                DtFilterUser.Rows[12]["TF_FieldName"] = "Search_Targets";
                DtFilterUser.Rows[12]["TF_Condition"] = Filters.Filter_SearchTargets;
                DtFilterUser.Rows[12]["TF_Type"] = "0";
            }


            var _Users = await _UnitOfWorkUCService._IUserService.GetAllUser_SP(DtFilterUser, LangID, _UserID, TenantID);
            if (_Users == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var TotalCount = _Users.FirstOrDefault();
            int _Count = 0;
            if (TotalCount != null) _Count = Convert.ToInt32(TotalCount.Total_Count);
            return Ok(new { Users = _Users, Actions = _Actions, Count = _Count, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetByUserProfile")]
        [HttpPost]
        public async Task<IActionResult> GetByUserProfile([FromForm] int LangID)
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID <= 0) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetUser_By_ID_SP(_UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _UserRole = await _UnitOfWorkUCService._IUserRoleService.GetAllRole_By_UserID_SP(_UserID, LangID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.Usr_TnatID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _UserID && A.Annex_IsDefault == true);
            var _Path = "";
            if (_Annex != null) _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            return Ok(new { User = _User, _Role = _UserRole, PathImg = _Path, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("ChangePasswordUser")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordUser([FromForm] string OldPass, [FromForm] string NewPass)
        {
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID <= 0) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            string _OldPassHash = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(OldPass);
            if (_OldPassHash != _User.User_HashPassword) return Ok(new { Message = MessageException.Messages.NotMatchPassWord.ToString(), Status = MessageException.Status.Status400 });
            _User.User_HashPassword = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(NewPass);
            _UnitOfWorkUCService._IUserService.Update(_User);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_GetByID)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int UserID)
        {
            if (UserID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _User = (await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == UserID));
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.User_ID && A.Annex_IsDefault == true);
            var _Path = "";
            if (_Annex != null) _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            _User.User_HashPassword = "";
            _User.User_IsActive = false;
            _User.User_IsBlock = false;
            var User = _IMapperUser.Map<User, DtoUser>(_User);
            return Ok(new { User = User, PathImg = _Path, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("CreatePassTemp")]
        [HttpGet]
        public IActionResult CreatePassTemp()
        {
            var _Pass = _UnitOfWorkUCService._IUserService.CreatePasswordTemporary();
            if (_Pass == "") return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Pass = _Pass, Status = MessageException.Status.Status200 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoUser DtoUser, [FromForm] IFormFile IFileUser, [FromForm] string RolesID, [FromForm] string RolUsrPerm)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            List<DtoRoleUserPermission> _RolUsrPerm = JsonConvert.DeserializeObject<List<DtoRoleUserPermission>>(RolUsrPerm);
            if (DtoUser == null) return Ok(new { Message = MessageException.Messages.RequestNullUser.ToString(), Status = MessageException.Status.Status400 });
            if (RolesID == "") return Ok(new { Message = MessageException.Messages.RequestNullRole.ToString(), Status = MessageException.Status.Status400 });
            if ((await _UnitOfWorkUCService._IUserService.CheckUserName(TenantID, DtoUser.Usr_UName))) return Ok(new { Message = MessageException.Messages.FoundUserName.ToString(), Status = MessageException.Status.Status400 });
            if ((await _UnitOfWorkUCService._IUserService.CheckUserMobile(TenantID, DtoUser.Usr_Mobile))) return Ok(new { Message = MessageException.Messages.FoundUserMobile.ToString(), Status = MessageException.Status.Status400 });
            var _User = _IMapperUser.Map<DtoUser, User>(DtoUser);
            _User.User_DateRegister = DateTime.Now;
            _User.User_HashPassword = _UnitOfWorkUCService._IAuthenticationService.CreateHashing(DtoUser.Usr_HPass);
            _User.User_TenantID = TenantID;
            _User.User_IsActive = false;
            _User.User_IsBlock = false;
            await _UnitOfWorkUCService._IUserService.Insert(_User);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
            {
                var User_Id = (await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_TenantID == TenantID && U.User_Mobile == DtoUser.Usr_Mobile)).User_ID;
                List<UserRole> _UserRole = new List<UserRole>();
                var Roles = RolesID.Split(',');
                if (Roles != null)
                {
                    foreach (var Role in Roles)
                    {
                        _UserRole.Add(new UserRole { Role_ID = Convert.ToInt32(Role), User_ID = User_Id });
                    }
                    await _UnitOfWorkUCService._IUserRoleService.InsertRange(_UserRole);
                    await _UnitOfWorkUCService.SaveChange_DataBase_Async();
                }
                if (RolUsrPerm != null)
                {
                    List<RoleUserPermission> _RoleUserPermission = new List<RoleUserPermission>();
                    foreach (var RolePer in _RolUsrPerm)
                    {
                        _RoleUserPermission.Add(
                            new RoleUserPermission
                            {
                                RoleUserPermission_RoleID_UserID = User_Id,
                                RoleUserPermission_BitMerge = RolePer.RlUsrPer_BitMrge,
                                RoleUserPermission_ParentID = RolePer.RlUsrPer_ParentID,
                                RoleUserPermission_RouteStructureID = RolePer.RlUsrPer_RoutStrID,
                            });
                    }
                    if (_RoleUserPermission.Count() > 0)
                    {
                        await _UnitOfWorkUCService._IRoleUserPermissionService.InsertRange(_RoleUserPermission);
                        await _UnitOfWorkUCService.SaveChange_DataBase_Async();
                    }
                }

                var TextMessage = "homacall";
                var _Sms = await _UnitOfWorkUCService._ISmsService.Send("", DtoUser.Usr_Mobile, DtoUser.Usr_HPass, TextMessage);

                if (IFileUser.Length > 0)
                {
                    string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                    var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "USRA");
                    if (_DtoAnnexSetting == null) return Ok(new { Message = MessageException.Messages.NoFindPath.ToString(), Status = MessageException.Status.Status400 });
                    var _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + User_Id;
                    var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                    if (!_boolDirectory) return Ok(new { Message = MessageException.Messages.NoDirectory.ToString(), Status = MessageException.Status.Status400 });
                    var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileUser.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileUser.FileName), _Path);
                    if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser, Status = MessageException.Status.Status400 });
                    Annexs _Annex = new Annexs()
                    {
                        Annex_Path = _Path.Replace("//", "/"),
                        Annex_FileNamePhysicy = _PhysicyNameFile,
                        Annex_FileNameLogic = IFileUser.FileName,
                        Annex_ReferenceID = User_Id,
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

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_Update)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoUser DtoUser)
        {
            if (DtoUser == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _User = (await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == DtoUser.Usr_ID));
            if (_User != null)
            {
                _User.User_FirstName = DtoUser.Usr_FName;
                _User.User_LastName = DtoUser.Usr_LName;
                _User.User_IdentifyNumber = DtoUser.Usr_IdentNum;
                _User.User_Gender = DtoUser.Usr_Gender;
                _User.User_Email = DtoUser.Usr_mail;
                if (DtoUser.Usr_UName != null)
                {
                    _User.User_UserName = DtoUser.Usr_UName;
                    if ((await _UnitOfWorkUCService._IUserService.CheckUserName(TenantID, DtoUser.Usr_UName))) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
                }
                if (DtoUser.Usr_Mobile != null)
                {
                    _User.User_Mobile = DtoUser.Usr_Mobile;
                    if ((await _UnitOfWorkUCService._IUserService.CheckUserMobile(TenantID, DtoUser.Usr_Mobile))) return Ok(new { Message = MessageException.Messages.FoundUserMobile.ToString(), Status = MessageException.Status.Status400 });
                }
                // if (DtoUser.Usr_HPass != "") _User.Usr_HPass = _unitOfWork._IAuthentication.CreateHashing(DtoUser.Usr_HPass);
                _User.User_Province_ID = DtoUser.Usr_Prov_ID;
                _User.User_City_ID = DtoUser.Usr_Cty_ID;
                _User.User_Address = DtoUser.Usr_Address;

                _UnitOfWorkUCService._IUserService.Update(_User);
                if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_ISBlock)]
        [Route("ISBlock")]
        [HttpPost]
        public async Task<IActionResult> ISBlock([FromForm] int UserID, [FromForm] bool ISBlock)
        {
            if (UserID < 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _State = await _UnitOfWorkUCService._IUserService.IsBlockUser(UserID, ISBlock);
            if (_State == false) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_ISActive)]
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


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_UploadImage)]
        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] int UserID, [FromForm] IFormFile IFileUser)
        {
            if (UserID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "USRA");
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
                var _PathNew = (_Path + "//" + _PhysicyNameFile + "" + Path.GetExtension(IFileUser.FileName)).Replace("//", "/");
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Path = _PathNew, Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
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
                _Path = (_Path + "//" + Annex_.Annex_FileNamePhysicy + "" + Annex_.Annex_FileExtension).Replace("//", "/");
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Path = _Path, Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_User, EnumPermission.Actions.Action_UC_User_DeleteImage)]
        [Route("DeleteImage")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage([FromForm] int UserID)
        {
            if (UserID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == UserID);
            if (_User == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_User.User_TenantID, "USRA");
            var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _User.User_ID);
            var _Path = "";
            if (_Annex != null) _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            if (await _UnitOfWorkAnnexService._IFtpService.Exists(_Path))
            {
                await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
                if (_Annex != null)
                {
                    _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
                    if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
                }
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeCommon]
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
                    if (Token == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
                    _UnitOfWorkUCService._ITokenService.Delete(Token);
                    if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
                }
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}

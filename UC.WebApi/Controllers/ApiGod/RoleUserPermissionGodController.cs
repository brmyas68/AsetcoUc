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
    public class RoleUserPermissionGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapperRoleUserPermission; private readonly IConfiguration _Configuration;
        public RoleUserPermissionGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperRoleUserPermission = MapperRoleUserPermission.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int UsrRol_ID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _UserRoleActions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_PermissionUserRoutePath(LanguageID, UsrRol_ID);
            if (_UserRoleActions == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { UserRoleActions = _UserRoleActions, Status = MessageException.Status.Status200 });

        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllActions")]
        [HttpPost]
        public async Task<IActionResult> GetAllActions([FromForm] int UsrRol_ID, [FromForm] int FormID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _UserRoleActions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_PermissionUserActions(LanguageID, UsrRol_ID, FormID);
            if (_UserRoleActions == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { UserRoleActions = _UserRoleActions, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] List<DtoRoleUserPermission> RolUsrPerm)
        {
            if (RolUsrPerm == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleUserPermission = RolUsrPerm.Select(RolPer => _IMapperRoleUserPermission.Map<DtoRoleUserPermission, RoleUserPermission>(RolPer)).ToList();
            await _UnitOfWorkUCService._IRoleUserPermissionService.InsertRange(_RoleUserPermission);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int UserID, [FromForm] int ParentID)
        {
            if (UserID <= 0 && ParentID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleUserPermissions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll(RUP => RUP.RoleUserPermission_RoleID_UserID == UserID && RUP.RoleUserPermission_ParentID == ParentID);
            if (_RoleUserPermissions == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IRoleUserPermissionService.DeleteRange(_RoleUserPermissions);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.Common.Authorization;
using UC.Common.Enum;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;
using UC.Common;



namespace UC.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserPermissionController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private readonly IConfiguration _Configuration; IMapper _IMapperRoleUserPermission;
        public RoleUserPermissionController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperRoleUserPermission = MapperRoleUserPermission.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_RoleUserPermission, EnumPermission.Actions.Action_UC_RoleUserPermission_GetAll)]
        // [AuthorizeCommon]
        [Route("GetAll")] //GetAllPermissionUserRoutePath
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int UsrRol_ID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(L => L.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _DtoPermissionUserRoutePath = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_PermissionUserRoutePath(LanguageID, UsrRol_ID);
            if (_DtoPermissionUserRoutePath == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { PerminUserRoutPath = _DtoPermissionUserRoutePath, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllPermissionUserActions")]
        [HttpPost]
        public async Task<IActionResult> GetAllPermissionUserActions([FromForm] int UsrRol_ID, [FromForm] int FormID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(L => L.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _UserRoleActions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_PermissionUserActions(LanguageID, UsrRol_ID, FormID);
            if (_UserRoleActions == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { UserRoleActions = _UserRoleActions, Status = MessageException.Status.Status200 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_RoleUserPermission, EnumPermission.Actions.Action_UC_RoleUserPermission_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] List<DtoRoleUserPermission> RolUsrPerm)
        {
            if (RolUsrPerm == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var LisRolUserPer = RolUsrPerm.Select(RP => _IMapperRoleUserPermission.Map<DtoRoleUserPermission, RoleUserPermission>(RP)).ToList();
            await _UnitOfWorkUCService._IRoleUserPermissionService.InsertRange(LisRolUserPer);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_RoleUserPermission, EnumPermission.Actions.Action_UC_RoleUserPermission_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int UserID, [FromForm] int ParentID)
        {
            if (UserID <= 0 && ParentID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleUserPermissions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetByWhere(RP => RP.RoleUserPermission_RoleID_UserID == UserID && RP.RoleUserPermission_ParentID == ParentID);
            if (_RoleUserPermissions == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IRoleUserPermissionService.Delete(_RoleUserPermissions);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


    }
}


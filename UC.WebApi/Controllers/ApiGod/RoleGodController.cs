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
    public class RoleGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public RoleGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperRole.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _Roles = await _UnitOfWorkUCService._IRoleService.GetAllRole_By_SP(LanguageID, TenantID);
            if (_Roles == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Roles = _Roles, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var Role = await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_ID == ID);
            var _Role = _IMapper.Map<Role, DtoRole>(Role);
            return Ok(new { Role = _Role, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoRole DtoRole)
        {
            if (DtoRole == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Role = _IMapper.Map<DtoRole, Role>(DtoRole);
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            _Role.Role_TenantID = TenantID;
            await _UnitOfWorkUCService._IRoleService.Insert(_Role);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

       // [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] DtoRole DtoRole)
        {
            if (DtoRole == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Role = await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_ID == DtoRole.Rol_ID);
            if (_Role != null)
            {
                _Role.Role_TagID = DtoRole.Rol_TgID;
                _Role.Role_IsSystemRole = DtoRole.Rol_IsSysRol;
                _Role.Role_ReadOnly = DtoRole.Rol_RedOnly;
                _UnitOfWorkUCService._IRoleService.Update(_Role);
                if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Role = await _UnitOfWorkUCService._IRoleService.GetByWhere(R => R.Role_ID == ID);
            if (_Role == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IRoleService.Delete(_Role);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}
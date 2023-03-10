using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
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
    public class RoleMembersGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public RoleMembersGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperRoleMembers.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var RoleMembers = await _UnitOfWorkUCService._IRoleMembersService.GetAll();
            if (RoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleMembers = RoleMembers.Select(R => _IMapper.Map<RoleMembers, DtoRoleMembers>(R)).ToList();
            return Ok(new { RoleMembers = _RoleMembers, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllMember")]
        [HttpPost]
        public async Task<IActionResult> GetAllMember([FromForm] int RolID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _RoleMembers = await _UnitOfWorkUCService._IRoleMembersService.GetAllRoleMembers_By_SP(RolID, LanguageID, TenantID);
            if (_RoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RoleMembers = _RoleMembers, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllListMember")]
        [HttpPost]
        public async Task<IActionResult> GetAllListMember([FromForm] int RolID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _RoleMembers = await _UnitOfWorkUCService._IRoleMembersService.ListRoleMembers_By_SP(RolID, LanguageID, TenantID);
            if (_RoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RoleMembers = _RoleMembers, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int RolMemID)
        {
            if (RolMemID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var RoleMembers = await _UnitOfWorkUCService._IRoleMembersService.GetByWhere(R => R.RoleMembers_ID == RolMemID);
            var _RoleMembers = _IMapper.Map<RoleMembers, DtoRoleMembers>(RoleMembers);
            return Ok(new { Role = _RoleMembers, Status = MessageException.Status.Status200 });

        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] List<DtoRoleMembers> _DtoRoleMembers)
        {
            if (_DtoRoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleMembers = _DtoRoleMembers.Select(R => _IMapper.Map<DtoRoleMembers, RoleMembers>(R)).ToList();
            await _UnitOfWorkUCService._IRoleMembersService.InsertRange(_RoleMembers);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoRoleMembers DtoRoleMembers)
        {
            if (DtoRoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RoleMembers = await _UnitOfWorkUCService._IRoleMembersService.GetByWhere(R => R.RoleMembers_ID == DtoRoleMembers.RolMbrs_ID);
            if (_RoleMembers != null)
            {
                _RoleMembers.RoleMembers_RoleID = DtoRoleMembers.RolMbrs_RolID;
                _RoleMembers.RoleMembers_RoleMemberID = DtoRoleMembers.RolMbrs_RolMberID;

                _UnitOfWorkUCService._IRoleMembersService.Update(_RoleMembers);
                if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int RolMemID)
        {
            if (RolMemID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoRoleMembers = await _UnitOfWorkUCService._IRoleMembersService.GetByWhere(R => R.RoleMembers_ID == RolMemID);
            if (_DtoRoleMembers == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IRoleMembersService.Delete(_DtoRoleMembers);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

        }

    }
}



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
    public class RouteStructureGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public RouteStructureGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperRouteStructure.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll();
            if (RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = RouteStructures.Select(R => _IMapper.Map<RouteStructure, DtoRouteStructure>(R)).ToList();
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllRouteStructureTreeByTagID")]
        [HttpPost]
        public async Task<IActionResult> GetAllRouteStructureTreeByTagID([FromForm] int TagID, [FromForm] int? TypeRout)
        {
            var _TypeRout = 0;
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (TypeRout == null) _TypeRout = 0;
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_Route_RouteStructureTreeByTagID(LanguageID, TagID, TenantID, _TypeRout);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructuresTree = _RouteStructures, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var RouteStructure = await _UnitOfWorkUCService._IRouteStructureService.GetByWhere(R => R.RouteStructure_ID == ID);
            var _RouteStructure = _IMapper.Map<RouteStructure, DtoRouteStructure>(RouteStructure);
            return Ok(new { RouteStructure = _RouteStructure, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetRSIDByTagID")]
        [HttpPost]
        public async Task<IActionResult> GetRSIDByTagID([FromForm] int TagID)
        {
            if (TagID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructureID = (await _UnitOfWorkUCService._IRouteStructureService.GetByWhere(R => R.RouteStructure_Tagsknowledge_ID == TagID))?.RouteStructure_ID;
            if (_RouteStructureID == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RSID = _RouteStructureID.Value, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByIDSP")]
        [HttpPost]
        public async Task<IActionResult> GetByIDSP([FromForm] int RotID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0 && RotID <= 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetRouteStructure_By_ID_SP(LanguageID, RotID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByTypeRouteForTree")]
        [HttpGet]
        public async Task<IActionResult> GetAllByTypeRouteForTree()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_TypeRoute_For_Tree(LanguageID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByTypeRoute")]
        [HttpGet]
        public async Task<IActionResult> GetAllByTypeRoute()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_TypeRoute(LanguageID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByTypeRoute")]
        [HttpPost]
        public async Task<IActionResult> GetAllByTypeRoute([FromForm] int TypRotID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_TypeRoute(LanguageID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByParent")]
        [HttpPost]
        public async Task<IActionResult> GetAllByParent([FromForm] int ParntID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_Parent_ID(LanguageID, ParntID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByParentTypeRoute")]
        [HttpPost]
        public async Task<IActionResult> GetAllByParentTypeRoute([FromForm] int ParntID, [FromForm] int TypRotID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (LanguageID <= 0 && ParntID < 0 && TypRotID < 0) Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_Parent_TypeRoute_ID(LanguageID, ParntID, TypRotID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RouteStructures = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoRouteStructure DtoRouteStructure)
        {
            if (DtoRouteStructure == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructure = _IMapper.Map<DtoRouteStructure, RouteStructure>(DtoRouteStructure);
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            _RouteStructure.RouteStructure_TenantID = TenantID;
            await _UnitOfWorkUCService._IRouteStructureService.Insert(_RouteStructure);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoRouteStructure DtoRouteStructure)
        {
            if (DtoRouteStructure == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructure = await _UnitOfWorkUCService._IRouteStructureService.GetByWhere(R => R.RouteStructure_ID == DtoRouteStructure.RoutStr_ID);
            if (_RouteStructure != null)
            {
                _RouteStructure.RouteStructure_ID = DtoRouteStructure.RoutStr_PID;
                _RouteStructure.RouteStructure_Tagsknowledge_ID = DtoRouteStructure.RoutStr_Tag_ID;
                _RouteStructure.RouteStructure_TypeRoute = DtoRouteStructure.RoutStr_TypeRout;

                _UnitOfWorkUCService._IRouteStructureService.Update(_RouteStructure);
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
            var _DtoRouteStructure = await _UnitOfWorkUCService._IRouteStructureService.GetByWhere(R => R.RouteStructure_ID == ID);
            if (_DtoRouteStructure == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IRouteStructureService.Delete(_DtoRouteStructure);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

        }

    }
}

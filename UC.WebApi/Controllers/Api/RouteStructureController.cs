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
using UC.ClassDTO.DTOs.Custom;


namespace UC.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RouteStructureController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private readonly IConfiguration _Configuration; IMapper _IMapperRouteStructure;
        public RouteStructureController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperRouteStructure = MapperRouteStructure.MapTo(); }

        [AuthorizeCommon]
        [Route("GetAllByTypeRouteForTree")]
        [HttpPost]
        public async Task<IActionResult> GetAllByTypeRouteForTree([FromForm] int LanguageID)
        {
            if (LanguageID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_TypeRoute_For_Tree(LanguageID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RoutStrucs = _RouteStructures, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllByParent")]
        [HttpPost]
        public async Task<IActionResult> GetAllByParent([FromForm] int ParntID, [FromForm] int LanguageID)
        {
            if (LanguageID <= 0 && ParntID < 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _RouteStructures = await _UnitOfWorkUCService._IRouteStructureService.GetAll_By_Parent_ID(LanguageID, ParntID);
            if (_RouteStructures == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { RoutStrucs = _RouteStructures, Status = MessageException.Status.Status200 });
        }
    }
}



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
    public class MenuLinkGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; readonly IConfiguration _Configuration; IMapper _IMapper;
        public MenuLinkGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperMenuLink.MapTo(); }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllGrid")]
        [HttpGet]
        public async Task<IActionResult> GetAll_Grid()
        {
            var _MenuLinksGrid = await _UnitOfWorkUCService._IMenuLinkService.GetAll_Grid();
            if (_MenuLinksGrid == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { MenuLinksGrid = _MenuLinksGrid, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int SysTagID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _MenuLinks = await _UnitOfWorkUCService._IMenuLinkService.GetAll_SP(LanguageID, SysTagID);
            if (_MenuLinks == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { MenuLinks = _MenuLinks, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllLinkSubMenu")]
        [HttpPost]
        public async Task<IActionResult> GetAllLinkSubMenu([FromForm] int SysTagID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _MenuLinks = await _UnitOfWorkUCService._IMenuLinkService.GetAllSubMenu_SP(LanguageID, SysTagID);
            if (_MenuLinks == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { MenuLinks = _MenuLinks, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoMenuLink MenuLnk)
        {
            if (MenuLnk == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _MenuLink = _IMapper.Map<DtoMenuLink, MenuLink>(MenuLnk);
            await _UnitOfWorkUCService._IMenuLinkService.Insert(_MenuLink);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int LinkId)
        {
            if (LinkId <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _MenuLink = await _UnitOfWorkUCService._IMenuLinkService.GetByWhere(M => M.MenuLink_ID == LinkId);
            if (_MenuLink == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IMenuLinkService.Delete(_MenuLink);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }


    }
}

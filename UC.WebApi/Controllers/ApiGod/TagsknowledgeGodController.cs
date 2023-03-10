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
    public class TagsknowledgeGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public TagsknowledgeGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperTagsknowledge.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int TagType)
        {
            if (TagType == -1)
            {
                var Tagsknowledges = await _UnitOfWorkUCService._ITagsknowledgeService.GetAll();
                var _Tagsknowledges = Tagsknowledges.Select(T => _IMapper.Map<Tagsknowledge, DtoTagsknowledge>(T)).ToList();
                return Ok(new { Tagsknowledges = _Tagsknowledges, Status = MessageException.Status.Status200 });
            }
            else
            {
                var _Tagsknowledges = await _UnitOfWorkUCService._ITagsknowledgeService.GetAllRole_SP(TagType);
                return Ok(new { Tagsknowledges = _Tagsknowledges, Status = MessageException.Status.Status200 });
            }
            // return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllTagsTranslate")]
        [HttpPost]
        public async Task<IActionResult> GetAllTagsTranslate([FromForm] int TagType, [FromForm] int ParentID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _Tagsknowledges = await _UnitOfWorkUCService._ITagsknowledgeService.GetAll_Tags_Translate(LanguageID, TagType, ParentID);
            if (_Tagsknowledges == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Tagsknowledges = _Tagsknowledges, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var Tagsknowledge = await _UnitOfWorkUCService._ITagsknowledgeService.GetByWhere(T => T.Tagsknowledge_ID == ID);
            var _Tagsknowledge = _IMapper.Map<Tagsknowledge, DtoTagsknowledge>(Tagsknowledge);
            return Ok(new { Tagsknowledge = _Tagsknowledge, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoTagsknowledge DtoTagsknowledge)
        {
            if (DtoTagsknowledge == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Tagsknowledge = _IMapper.Map<DtoTagsknowledge, Tagsknowledge>(DtoTagsknowledge);
            await _UnitOfWorkUCService._ITagsknowledgeService.Insert(_Tagsknowledge);

            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
            {
                if (_Tagsknowledge.Tagsknowledge_Type == TagType.System)
                {
                    var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                    var _RouteStructure = new RouteStructure()
                    {
                        RouteStructure_TenantID = TenantID,
                        RouteStructure_Tagsknowledge_ID = _Tagsknowledge.Tagsknowledge_ID,
                        RouteStructure_ParentID = 0,
                        RouteStructure_TypeRoute = 0
                    };
                    await _UnitOfWorkUCService._IRouteStructureService.Insert(_RouteStructure);
                    await _UnitOfWorkUCService.SaveChange_DataBase_Async();
                }
                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }

            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoTagsknowledge DtoTagsknowledge)
        {
            if (DtoTagsknowledge == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Tagsknowledge = await _UnitOfWorkUCService._ITagsknowledgeService.GetByWhere(T => T.Tagsknowledge_ID == DtoTagsknowledge.Tag_ID);
            if (_Tagsknowledge != null)
            {
                _Tagsknowledge.Tagsknowledge_Name = DtoTagsknowledge.Tag_Name;
                _Tagsknowledge.Tagsknowledge_Type = DtoTagsknowledge.Tag_Type;
                _Tagsknowledge.Tagsknowledge_ParentID = DtoTagsknowledge.Tag_PID;
                _UnitOfWorkUCService._ITagsknowledgeService.Update(_Tagsknowledge);
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
            var _DtoTagsknowledge = await _UnitOfWorkUCService._ITagsknowledgeService.GetByWhere(T => T.Tagsknowledge_ID == ID);
            if (_DtoTagsknowledge == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._ITagsknowledgeService.Delete(_DtoTagsknowledge);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }



    }
}
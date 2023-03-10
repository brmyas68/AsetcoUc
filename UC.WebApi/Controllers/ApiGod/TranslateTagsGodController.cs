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
    public class TranslateTagsGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public TranslateTagsGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperTranslateTags.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var TranslateTags = await _UnitOfWorkUCService._ITranslateTagsService.GetAll();
            if (TranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _TranslateTags = TranslateTags.Select(T => _IMapper.Map<TranslateTags, DtoTranslateTags>(T)).ToList();
            return Ok(new { TranslateTags = _TranslateTags, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var TranslateTag = await _UnitOfWorkUCService._ITranslateTagsService.GetByWhere(T => T.TranslateTags_ID == ID);
            var _TranslateTag = _IMapper.Map<TranslateTags, DtoTranslateTags>(TranslateTag);
            return Ok(new { TranslateTag = _TranslateTag, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByLangID")]
        [HttpPost]
        public async Task<IActionResult> GetAllByLangID([FromForm] int Lang_ID)
        {
            if (Lang_ID <= 0) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var TranslateTag = await _UnitOfWorkUCService._ITranslateTagsService.GetAll_By_Language_ID(Lang_ID);
            return Ok(new { TranslateTag = TranslateTag, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByTagID")]
        [HttpPost]
        public async Task<IActionResult> GetAllByTagID([FromForm] int Tag_ID)
        {
            if (Tag_ID <= 0) Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var _TranslateTags = await _UnitOfWorkUCService._ITranslateTagsService.GetAll_By_Tagsknowledge_ID(Tag_ID);
            if (_TranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { TranslateTag = _TranslateTags, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAllByLangTagID")]
        [HttpPost]
        public async Task<IActionResult> GetAllByLangTagID([FromForm] int Lang_ID, [FromForm] int Tag_ID)
        {
            if (Lang_ID <= 0 && Tag_ID <= 0) Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var _TranslateTags = await _UnitOfWorkUCService._ITranslateTagsService.GetAll_By_Language_Tagsknowledge_ID(Lang_ID, Tag_ID);
            if (_TranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { TranslateTag = _TranslateTags, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoTranslateTags DtoTranslateTags)
        {
            if (DtoTranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _TranslateTags = _IMapper.Map<DtoTranslateTags, TranslateTags>(DtoTranslateTags);
            await _UnitOfWorkUCService._ITranslateTagsService.Insert(_TranslateTags);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoTranslateTags DtoTranslateTags)
        {
            if (DtoTranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _TranslateTag = await _UnitOfWorkUCService._ITranslateTagsService.GetByWhere(T => T.TranslateTags_ID == DtoTranslateTags.TranTg_ID);
            if (_TranslateTag != null)
            {
                _TranslateTag.TranslateTags_TagsknowledgeID = DtoTranslateTags.TranTg_TagID;
                _TranslateTag.TranslateTags_LanguageID = DtoTranslateTags.TranTg_LangID;
                _TranslateTag.TranslateTags_Text = DtoTranslateTags.TranTg_Text;

                _UnitOfWorkUCService._ITranslateTagsService.Update(_TranslateTag);
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
            var _DtoTranslateTags = await _UnitOfWorkUCService._ITranslateTagsService.GetByWhere(T => T.TranslateTags_ID == ID);
            if (_DtoTranslateTags == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._ITranslateTagsService.Delete(_DtoTranslateTags);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}
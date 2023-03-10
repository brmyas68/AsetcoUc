using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Filters;
using UC.Common.Authorization;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;

namespace UC.WebApi.Controllers.Api.Commons
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration; IMapper _IMapperProvinces; IMapper _IMapperlanguage; IMapper _IMapperCity;
        public CommonController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperProvinces = MapperProvince.MapTo(); _IMapperlanguage = MapperLanguage.MapTo(); _IMapperCity = MapperCity.MapTo(); }

        [AuthorizeCommon]
        [Route("CreateLinkMenu")]
        [HttpPost]
        public async Task<IActionResult> CreateLinkMenu([FromForm] int LangID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _LinkRoute = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAllLinkMenuPermission(_UserID, LangID, TenantID);
            return Ok(new { LinkRoute = _LinkRoute, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllListActions")] 
        [HttpGet]
        public async Task<IActionResult> GetAllListActions()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _ListActions = await _UnitOfWorkUCService._IRoleUserPermissionService.GetAll_ListActionsPermission(_UserID, TenantID);
            return Ok(new { ListActions = _ListActions, Status = MessageException.Status.Status200 });
        }


        [AuthorizeCommon]
        [Route("GetAllTagNamesForm")]
        [HttpPost]
        public async Task<IActionResult> GetAllTagNamesForm([FromForm] int LangID, [FromForm] string TagNameForm)
        {
            if (TagNameForm == "") return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _ListTagNames = await _UnitOfWorkUCService._IRouteStructureService.GetAll_Form_RouteStructure(LangID, TenantID, TagNameForm);
            return Ok(new { ListTagNames = _ListTagNames, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetAllProvince")]
        [HttpGet]
        public async Task<IActionResult> GetAllProvince()
        {
            var Provinces = await _UnitOfWorkUCService._IProvinceService.GetAll();
            if (Provinces == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Provinces = Provinces.Select(P => _IMapperProvinces.Map<Province, DtoProvince>(P)).ToList();
            return Ok(new { Provinces = _Provinces, Status = MessageException.Status.Status200 });
        }

        //[AuthorizeCommon]
        [Route("GetAllLanguage")]
        [HttpGet]
        public async Task<IActionResult> GetAllLanguage()
        {
            var language = await _UnitOfWorkUCService._ILanguageService.GetAll();
            if (language == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _language = language.Select(L => _IMapperlanguage.Map<Language, DtoLanguage>(L)).ToList();
            return Ok(new { language = _language, Status = MessageException.Status.Status200 });
        }

        //[AuthorizeCommon]
        [Route("DefaultLangCo")]
        [HttpGet]
        public async Task<IActionResult> DefaultLangCo()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _DefaultLangCo = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            if (_DefaultLangCo == -1) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { DefaultLangCo = _DefaultLangCo, Status = MessageException.Status.Status200 });
        }

        [AuthorizeCommon]
        [Route("GetByProvinceID")]
        [HttpPost]
        public async Task<IActionResult> GetByProvinceID([FromForm] int PrviceID)
        {
            if (PrviceID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var Cityies = await _UnitOfWorkUCService._ICityService.GetAll_By_ProvinceID(PrviceID);
            if (Cityies == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Cityies = Cityies.Select(C => _IMapperCity.Map<City, DtoCity>(C)).ToList();
            return Ok(new { Cityies = _Cityies, Status = MessageException.Status.Status200 });
        }

        [AllowAnonymous]
        [Route("GetAllListTagsTranslate")]
        [HttpPost]
        public async Task<IActionResult> GetAllListTagsTranslate([FromForm] string TagNameForm, [FromForm] int LanguageID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (LanguageID < 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _TagsTranslate = await _UnitOfWorkUCService._ITagsknowledgeService.GetAll_List_TagsTranslate(LanguageID, TagNameForm);
            if (_TagsTranslate == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });

            string _CompanyLogoPath = "";
            var _AnnexSettingsLGOCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOCA");
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSettingsLGOCA.AnnexSetting_ID && A.Annex_ReferenceID == TenantID );
            if (_Annex != null) _CompanyLogoPath = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;

            var _AnnexSettingsLGOUCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOUCA");
            var _UCLogoPath = await _UnitOfWorkAnnexService._IAnnexsService.GetAllPathFile(LanguageID, _AnnexSettingsLGOUCA.AnnexSetting_ID);

            return Ok(new { TagsTranslate = _TagsTranslate, CompanyLogoPath = _CompanyLogoPath , UCLogoPath = _UCLogoPath, Status = MessageException.Status.Status200 });
        }
    }
}

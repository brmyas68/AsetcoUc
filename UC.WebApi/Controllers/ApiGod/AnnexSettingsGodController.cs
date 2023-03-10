using Annex.ClassDomain.Domains;
using Annex.ClassDTO.DTOs;
using Annex.ClassDTO.DTOs.Customs;
using Annex.Common.Mapping;
using Annex.InterfaceService.InterfacesBase;
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
    public class AnnexSettingsGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IUnitOfWorkAnnexService _UnitOfWorkAnnexService; readonly IConfiguration _Configuration; IMapper _IMapperAnnexSetting;
        public AnnexSettingsGodController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapperAnnexSetting = MapperAnnexSetting.MapTo(); }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            var _AnnexSettings = await _UnitOfWorkAnnexService._IAnnexSettingService.GetAll_SP(TenantID);
            if (_AnnexSettings == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { AnnexSettings = _AnnexSettings, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int AnexSetingID)
        {
            if (AnexSetingID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var AnexSeting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetByWhere(A => A.AnnexSetting_ID == AnexSetingID);
            var _AnexSeting = _IMapperAnnexSetting.Map<AnnexSetting, DtoAnnexSetting>(AnexSeting);
            return Ok(new { AnexSeting = _AnexSeting, Status = MessageException.Status.Status200 });
        }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoAnnexSetting DtoAnexSeting)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (DtoAnexSeting == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _AnexSeting = _IMapperAnnexSetting.Map<DtoAnnexSetting, AnnexSetting>(DtoAnexSeting);
            _AnexSeting.AnnexSetting_TenantID = TenantID;
            await _UnitOfWorkAnnexService._IAnnexSettingService.Insert(_AnexSeting);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoAnnexSetting DtoAnexSeting)
        {
            if (DtoAnexSeting == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _AnexSeting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetByWhere(A => A.AnnexSetting_ID == DtoAnexSeting.AnxSeting_ID);
            if (_AnexSeting != null)
            {
                _AnexSeting.AnnexSetting_Dec = DtoAnexSeting.AnxSeting_Desc;
                _AnexSeting.AnnexSetting_KeyWord = DtoAnexSeting.AnxSeting_KeyWord;
                _AnexSeting.AnnexSetting_ReferenceComment = DtoAnexSeting.AnxSeting_RefComent;
                _AnexSeting.AnnexSetting_TagsknowledgeID = DtoAnexSeting.AnxSeting_TagID;
                _AnexSeting.AnnexSetting_SystemTagID = DtoAnexSeting.AnxSeting_SysTagID;

                _UnitOfWorkAnnexService._IAnnexSettingService.Update(_AnexSeting);
                if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int AnexSetingID)
        {
            if (AnexSetingID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _AnexSeting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetByWhere(A => A.AnnexSetting_ID== AnexSetingID);
            if (_AnexSeting  == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkAnnexService._IAnnexSettingService.Delete(_AnexSeting);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}
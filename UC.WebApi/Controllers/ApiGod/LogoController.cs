using Annex.ClassDomain.Domains;
using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing.Processors.Normalization;
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
    public class LogoController : Controller
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IUnitOfWorkAnnexService _UnitOfWorkAnnexService; readonly IConfiguration _Configuration;
        public LogoController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; }


        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            int LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(L => L.CompanyInfo_ID == TenantID)).CompanyInfo_LanguageID;
            var _AnnexSetting = (await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOUCA"));
            if (_AnnexSetting == null) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var Annexes = await _UnitOfWorkAnnexService._IAnnexsService.GetAllLogoAnnex(LanguageID, _AnnexSetting.AnnexSetting_ID);
            return Ok(new { Annexes = Annexes, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] int TagId, [FromForm] IFormFile IFileLogo)
        {
            if (TagId <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            if (_UnitOfWorkAnnexService._IAnnexsService.Any(A => A.Annex_ReferenceID == TagId)) return Ok(new { Message = MessageException.Messages.TagUsed.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (IFileLogo.Length > 0)
            {
                string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOUCA");
                if (_DtoAnnexSetting == null) return Ok(new { Message = MessageException.Messages.NoFindPath.ToString(), Status = MessageException.Status.Status400 });
                var _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + TagId;
                var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                if (!_boolDirectory) return Ok(new { Message = MessageException.Messages.NoDirectory.ToString(), Status = MessageException.Status.Status400 });
                var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileLogo.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileLogo.FileName), _Path);
                if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser, Status = MessageException.Status.Status400 });
                Annexs _Annex = new Annexs()
                {
                    Annex_Path = _Path.Replace("//", "/"),
                    Annex_FileNamePhysicy = _PhysicyNameFile,
                    Annex_FileNameLogic = IFileLogo.FileName,
                    Annex_ReferenceID = TagId,
                    Annex_CreatedDate = DateTime.Now,
                    Annex_Description = "",
                    Annex_ReferenceFolderName = "",
                    Annex_AnnexSettingID = _DtoAnnexSetting.AnnexSetting_ID,
                    Annex_FileExtension = Path.GetExtension(IFileLogo.FileName),
                };
                await _UnitOfWorkAnnexService._IAnnexsService.Insert(_Annex);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int AnnexID)
        {
            if (AnnexID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            // Delete File 
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ID == AnnexID);
            if (_Annex == null) return Ok(new { Message = MessageException.Messages.NullAnnex.ToString(), Status = MessageException.Status.Status400 });
            var _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
            _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status200 });
        }
    }
}

using Annex.ClassDomain.Domains;
using Annex.ClassDTO.DTOs;
using Annex.ClassDTO.DTOs.Customs;
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
    public class CompanyInfoGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IUnitOfWorkAnnexService _UnitOfWorkAnnexService; readonly IConfiguration _Configuration; IMapper _IMapper;
        public CompanyInfoGodController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; _IMapper = MapperCompanyInfo.MapTo(); }


        //[AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);

            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOCA");
            if (_DtoAnnexSetting == null) return Ok(new { Message = MessageException.Messages.NoFindPath.ToString(), Status = MessageException.Status.Status400 });

            var LanguageID = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == TenantID));
            var CompanyInfos = await _UnitOfWorkUCService._ICompanyInfoService.GetAll_SP(LanguageID.CompanyInfo_LanguageID, _DtoAnnexSetting.AnnexSetting_ID);
            if (CompanyInfos == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { CompanyInfos = CompanyInfos, Status = MessageException.Status.Status200 });
        }

        // [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var CompanyInfo = await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == ID);
            var _CompanyInfo = _IMapper.Map<CompanyInfo, DtoCompanyInfo>(CompanyInfo);

            var _AnnexSettings = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_CompanyInfo.CoIn_ID, "LGOCA");
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSettings.AnnexSetting_ID && A.Annex_ReferenceID == _CompanyInfo.CoIn_ID  );
            if (_Annex != null) _CompanyInfo.CoIn_Logo = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;

            return Ok(new { CompanyInfo = _CompanyInfo, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoCompanyInfo DtoCompanyInfo, [FromForm] IFormFile IFileLogo)
        {
            if (DtoCompanyInfo == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _CompanyInfo = _IMapper.Map<DtoCompanyInfo, CompanyInfo>(DtoCompanyInfo);
            await _UnitOfWorkUCService._ICompanyInfoService.Insert(_CompanyInfo);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0)
            {
                if (IFileLogo.Length > 0)
                {
                    var _DtoListFiles = new List<DtoListFiles>();
                    var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                    var _AnnexSettingLGOCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOCA");
                    var DtoFiles = new DtoListFiles()
                    {
                        FileNameLogic = IFileLogo.FileName,
                        ExtensionName = Path.GetExtension(IFileLogo.FileName),
                        FileNamePhysic = Guid.NewGuid().ToString("N"),
                        FileStream = IFileLogo.OpenReadStream(),
                        FileDesc = "Default Company Logo",
                        IsDefault = true,

                    };
                    _DtoListFiles.Add(DtoFiles);
                    var _path = (_AnnexSettingLGOCA.AnnexSetting_Path + "//" + _CompanyInfo.CompanyInfo_ID).ToString();
                    var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                    var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(_DtoListFiles, _path, _CompanyInfo.CompanyInfo_ID, _AnnexSettingLGOCA.AnnexSetting_ID);
                    await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                    await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                }

                return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            }

            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoCompanyInfo DtoCompanyInfo, [FromForm] IFormFile IFileLogo)
        {
            if (DtoCompanyInfo == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _CompanyInfo = await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == DtoCompanyInfo.CoIn_ID);
            if (_CompanyInfo != null)
            {
                _CompanyInfo.CompanyInfo_TagID = DtoCompanyInfo.CoIn_TagID;
                _CompanyInfo.CompanyInfo_Address = DtoCompanyInfo.CoIn_Address;
                _CompanyInfo.CompanyInfo_Phone = DtoCompanyInfo.CoIn_Phone;
                _CompanyInfo.CompanyInfo_Mobile = DtoCompanyInfo.CoIn_Mobile;
                _CompanyInfo.CompanyInfo_Fax = DtoCompanyInfo.CoIn_Fax;
                _CompanyInfo.CompanyInfo_Email = DtoCompanyInfo.CoIn_Email;
                _CompanyInfo.CompanyInfo_Site = DtoCompanyInfo.CoIn_Site;
                _CompanyInfo.CompanyInfo_Instagram = DtoCompanyInfo.CoIn_Instagram;
                _CompanyInfo.CompanyInfo_About = DtoCompanyInfo.CoIn_About;
                _CompanyInfo.CompanyInfo_LanguageID = DtoCompanyInfo.CoIn_LangID;
                _CompanyInfo.CompanyInfo_SmsNumber = DtoCompanyInfo.CoIn_SmsNumber;
                _CompanyInfo.CompanyInfo_TypeDateTime = DtoCompanyInfo.CoIn_TypeDateTime;
                if (IFileLogo.Length > 0)
                {

                    var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_CompanyInfo.CompanyInfo_ID, "LGOCA");
                    var _AnnexSeting_ID = _DtoAnnexSetting.AnnexSetting_ID;
                    var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _AnnexSeting_ID && A.Annex_ReferenceID == _CompanyInfo.CompanyInfo_ID && A.Annex_IsDefault == true);
                    var _Path = "";
                    if (_Annex != null)
                    {
                        string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                        _Path = _Annex.Annex_Path.Replace("/", "//");
                        var _PathExists = _Path + "//" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
                        if (await _UnitOfWorkAnnexService._IFtpService.Exists(_PathExists))
                        {
                            await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_PathExists);
                        }
                        var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileLogo.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileLogo.FileName), _Path);
                        if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                        _Annex.Annex_FileNameLogic = IFileLogo.FileName;
                        _Annex.Annex_FileExtension = Path.GetExtension(IFileLogo.FileName);
                        _Annex.Annex_FileNamePhysicy = _PhysicyNameFile;
                        _UnitOfWorkAnnexService._IAnnexsService.Update(_Annex);
                        await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                    }
                    else
                    {
                        string _PhysicyNameFile = Guid.NewGuid().ToString("N");
                        _Path = _DtoAnnexSetting.AnnexSetting_Path + "//" + _CompanyInfo.CompanyInfo_ID;
                        var _boolDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_Path);
                        var _boolUpload = await _UnitOfWorkAnnexService._IFtpService.UploadImage(IFileLogo.OpenReadStream(), _PhysicyNameFile, Path.GetExtension(IFileLogo.FileName), _Path);
                        if (!_boolUpload) return Ok(new { Message = MessageException.Messages.NoUploadUser.ToString(), Status = MessageException.Status.Status400 });
                        Annexs Annex_ = new Annexs()
                        {
                            Annex_Path = _Path.Replace("//", "/"),
                            Annex_FileNamePhysicy = _PhysicyNameFile,
                            Annex_FileNameLogic = IFileLogo.FileName,
                            Annex_ReferenceID = _CompanyInfo.CompanyInfo_ID,
                            Annex_CreatedDate = DateTime.Now,
                            Annex_Description = "",
                            Annex_ReferenceFolderName = _PhysicyNameFile,
                            Annex_AnnexSettingID = _DtoAnnexSetting.AnnexSetting_ID,
                            Annex_FileExtension = Path.GetExtension(IFileLogo.FileName),
                            Annex_IsDefault = true
                        };
                        await _UnitOfWorkAnnexService._IAnnexsService.Insert(Annex_);
                        await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
                    }
                }

                _UnitOfWorkUCService._ICompanyInfoService.Update(_CompanyInfo);
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
            var _CompanyInfo = await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C => C.CompanyInfo_ID == ID);
            if (_CompanyInfo == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });

            var _DtoAnnexSetting = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(_CompanyInfo.CompanyInfo_ID, "LGOCA");
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_AnnexSettingID == _DtoAnnexSetting.AnnexSetting_ID && A.Annex_ReferenceID == _CompanyInfo.CompanyInfo_ID && A.Annex_IsDefault == true);
            if (_Annex != null)
            {
                var _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
                await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
                _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }
            
            _UnitOfWorkUCService._ICompanyInfoService.Delete(_CompanyInfo);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("UploadCompanyLogo")]
        [HttpPost]
        public async Task<IActionResult> UploadCompanyLogo([FromForm] string TagName, [FromForm] IFormFile IFileLogo)
        {
            if (IFileLogo.Length > 0)
            {
                var _DtoListFiles = new List<DtoListFiles>();
                var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
                var _AnnexSettingLGOCA = await _UnitOfWorkAnnexService._IAnnexSettingService.GetPath(TenantID, "LGOCA");
                var DtoFiles = new DtoListFiles()
                {
                    FileNameLogic = IFileLogo.FileName,
                    ExtensionName = Path.GetExtension(IFileLogo.FileName),
                    FileNamePhysic = Guid.NewGuid().ToString("N"),
                    FileStream = IFileLogo.OpenReadStream(),
                    FileDesc = TagName,

                };
                _DtoListFiles.Add(DtoFiles);
                var _path = (_AnnexSettingLGOCA.AnnexSetting_Path + "//" + TenantID).ToString();
                var _stateDirectory = await _UnitOfWorkAnnexService._IFtpService.CreateDirectory(_path.Replace("//", "/"));
                var AnnexList = await _UnitOfWorkAnnexService._IFtpService.UploadListFiles(_DtoListFiles, _path, TenantID, _AnnexSettingLGOCA.AnnexSetting_ID);
                await _UnitOfWorkAnnexService._IAnnexsService.InsertRange(AnnexList);
                await _UnitOfWorkAnnexService.SaveChange_DataBase_Async();
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("DeleteCompanyLogo")]
        [HttpPost]
        public async Task<IActionResult> DeleteCompanyLogo([FromForm] int AnnexID)
        {
            if (AnnexID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            // Delete File 
            var _Annex = await _UnitOfWorkAnnexService._IAnnexsService.GetByWhere(A => A.Annex_ID == AnnexID);
            if (_Annex == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Path = _Annex.Annex_Path + "/" + _Annex.Annex_FileNamePhysicy + "" + _Annex.Annex_FileExtension;
            await _UnitOfWorkAnnexService._IFtpService.DeleteImage(_Path);
            _UnitOfWorkAnnexService._IAnnexsService.Delete(_Annex);
            if (await _UnitOfWorkAnnexService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status200 });
        }

    }
}
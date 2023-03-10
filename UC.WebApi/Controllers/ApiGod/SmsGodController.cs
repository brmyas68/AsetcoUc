using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UC.ClassDTO.DTOs;
using UC.Common.Authorization;
using UC.Common.Enum;
using UC.Common.Exceptions;
using UC.Common.Mapping;
using UC.InterfaceService.InterfacesBase;
using UC.Service.ServiceBase;

namespace UC.WebApi.Controllers.ApiGod
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SmsGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper; private readonly IConfiguration _Configuration;
        public SmsGodController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapper = MapperRouteStructure.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Send")]
        [HttpPost]
        public async Task<IActionResult> Send([FromForm] string ReceptorMobile)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (ReceptorMobile == "") return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var SenderMobile = (await _UnitOfWorkUCService._ICompanyInfoService.GetByWhere(C=> C.CompanyInfo_ID== TenantID)).CompanyInfo_SmsNumber.ToString();
            if (SenderMobile == "") return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var ActiveCode = _UnitOfWorkUCService._ISmsService.GenerateActiveCode();
            var TextMessage = "homacall";
            if (ActiveCode <= 0) return Ok(new { Message = MessageException.Messages.Error.ToString(), Status = MessageException.Status.Status400 });
            var _Sms = await _UnitOfWorkUCService._ISmsService.Send(SenderMobile, ReceptorMobile, ActiveCode.ToString(), TextMessage);
            if (_Sms == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            await _UnitOfWorkUCService._ISmsService.Insert(_Sms);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });

        }
    }
}

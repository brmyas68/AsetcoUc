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
using  UC.Common;

namespace UC.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private readonly IConfiguration _Configuration; IMapper _IMapperRole;
        public RoleController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperRole = MapperRole.MapTo(); }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_Role, EnumPermission.Actions.Action_UC_Role_GetAll)]
        [Route("GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromForm] int LanguageID)
        {
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            if (LanguageID <= 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID <= 0) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _Roles = await _UnitOfWorkUCService._IRoleService.GetAllRole_By_SP(_UserID,LanguageID, TenantID);
            if (_Roles == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            return Ok(new { Roles = _Roles, Status = MessageException.Status.Status200 });
        }

    }
}


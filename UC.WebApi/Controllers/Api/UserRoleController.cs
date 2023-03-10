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


namespace UC.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private readonly IConfiguration _Configuration; IMapper _IMapperUserRole;
        public UserRoleController(IUnitOfWorkUCService UnitOfWorkUCService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _Configuration = Configuration; _IMapperUserRole = MapperUserRole.MapTo(); }


        [AuthorizePermission(EnumPermission.Controllers.Form_UC_UserRole, EnumPermission.Actions.Action_UC_UserRole_Insert)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] List<DtoUserRole> DtoUserRole)
        {
            if (DtoUserRole == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var LisUserRole = DtoUserRole.Select(UR => _IMapperUserRole.Map<DtoUserRole, UserRole>(UR)).ToList();
            await _UnitOfWorkUCService._IUserRoleService.InsertRange(LisUserRole);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizePermission(EnumPermission.Controllers.Form_UC_UserRole, EnumPermission.Actions.Action_UC_UserRole_Delete)]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int UserID)
        {
            if (UserID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _DtoRoles = await _UnitOfWorkUCService._IUserRoleService.GetByWhere(UR => UR.User_ID == UserID);
            if (_DtoRoles == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IUserRoleService.Delete(_DtoRoles);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}


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
    public class CityGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper;
        public CityGodController(IUnitOfWorkUCService UnitOfWorkUCService) { _UnitOfWorkUCService = UnitOfWorkUCService; _IMapper = MapperCity.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Cities = await _UnitOfWorkUCService._ICityService.GetAll();
            if (Cities == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _Cities = Cities.Select(C => _IMapper.Map<City, DtoCity>(C)).ToList();
            return Ok(new { Cities = _Cities, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var City = await _UnitOfWorkUCService._ICityService.GetByWhere(C => C.City_ID == ID);
            var _City = _IMapper.Map<City, DtoCity>(City);
            return Ok(new { City = _City, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByProvinceID")]
        [HttpPost]
        public async Task<IActionResult> GetByProvinceID([FromForm] int PrviceID)
        {
            if (PrviceID <= 0) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var Cities = await _UnitOfWorkUCService._ICityService.GetAll_By_ProvinceID(PrviceID);
            var _Cities = Cities.Select(C => _IMapper.Map<City, DtoCity>(C)).ToList();
            return Ok(new { Cities = _Cities, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoCity DtoCity)
        {
            if (DtoCity == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _City = _IMapper.Map<DtoCity, City>(DtoCity);
            await _UnitOfWorkUCService._ICityService.Insert(_City);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoCity DtoCity)
        {
            if (DtoCity == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _City = await _UnitOfWorkUCService._ICityService.GetByWhere(C => C.City_ID == DtoCity.Cty_ID);
            if (_City != null)
            {
                _City.City_Province_ID = DtoCity.Cty_Prv_ID;
                _City.City_Name = DtoCity.Cty_Name;
                _UnitOfWorkUCService._ICityService.Update(_City);
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
            var _City = await _UnitOfWorkUCService._ICityService.GetByWhere(C => C.City_ID == ID);
            if (_City == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._ICityService.Delete(_City);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }
    }
}

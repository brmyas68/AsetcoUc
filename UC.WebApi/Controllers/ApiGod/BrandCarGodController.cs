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
    public class BrandCarGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper;
        public BrandCarGodController(IUnitOfWorkUCService UnitOfWorkUCService) { _UnitOfWorkUCService = UnitOfWorkUCService; _IMapper = MapperBrandCar.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var BrandCars = await _UnitOfWorkUCService._IBrandCarService.GetAll();
            if (BrandCars == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _BrandCars = BrandCars.Select(B => _IMapper.Map<BrandCar, DtoBrandCar>(B)).ToList();
            return Ok(new { BrandCars = _BrandCars, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var BrandCar = await _UnitOfWorkUCService._IBrandCarService.GetByWhere(B => B.BrandCar_ID == ID);
            var _BrandCar = _IMapper.Map<BrandCar, DtoBrandCar>(BrandCar);
            return Ok(new { BrandCar = _BrandCar, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoBrandCar DtoBrandCar)
        {
            if (DtoBrandCar == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _BrandCar = _IMapper.Map<DtoBrandCar, BrandCar>(DtoBrandCar);
            await _UnitOfWorkUCService._IBrandCarService.Insert(_BrandCar);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoBrandCar DtoBrandCar)
        {
            if (DtoBrandCar == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _BrandCar = await _UnitOfWorkUCService._IBrandCarService.GetByWhere(P => P.BrandCar_ID == DtoBrandCar.BrdCar_ID);
            if (_BrandCar != null)
            {
                _BrandCar.BrandCar_Name = DtoBrandCar.BrdCar_Name;
                _UnitOfWorkUCService._IBrandCarService.Update(_BrandCar);
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
            var _BrandCar = await _UnitOfWorkUCService._IBrandCarService.GetByWhere(P => P.BrandCar_ID == ID);
            if (_BrandCar == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IBrandCarService.Delete(_BrandCar);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}


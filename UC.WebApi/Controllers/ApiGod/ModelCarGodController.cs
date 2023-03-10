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
    public class ModelCarGodController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; IMapper _IMapper;
        public ModelCarGodController(IUnitOfWorkUCService UnitOfWorkUCService) { _UnitOfWorkUCService = UnitOfWorkUCService; _IMapper = MapperModelCar.MapTo(); }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ModelCars = await _UnitOfWorkUCService._IModelCarService.GetAll();
            if (ModelCars == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ModelCars = ModelCars.Select(B => _IMapper.Map<ModelCar, DtoModelCar>(B)).ToList();
            return Ok(new { ModelCars = _ModelCars, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("GetByID")]
        [HttpPost]
        public async Task<IActionResult> GetByID([FromForm] int ID)
        {
            if (ID <= 0) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            var ModelCar = await _UnitOfWorkUCService._IModelCarService.GetByWhere(B => B.ModelCar_ID == ID);
            var _ModelCar = _IMapper.Map<ModelCar, DtoModelCar>(ModelCar);
            return Ok(new { ModelCar = _ModelCar, Status = MessageException.Status.Status200 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Insert")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] DtoModelCar DtoModelCar)
        {
            if (DtoModelCar == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ModelCar = _IMapper.Map<DtoModelCar, ModelCar>(DtoModelCar);
            await _UnitOfWorkUCService._IModelCarService.Insert(_ModelCar);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [AuthorizeGod(EnumPermission.Role.Role_UC_God)]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] DtoModelCar DtoModelCar)
        {
            if (DtoModelCar == null) return Ok(new { Message = MessageException.Messages.RequestNull.ToString(), Status = MessageException.Status.Status400 });
            var _ModelCar = await _UnitOfWorkUCService._IModelCarService.GetByWhere(P => P.ModelCar_ID == DtoModelCar.ModCar_ID);
            if (_ModelCar != null)
            {
                _ModelCar.ModelCar_Name = DtoModelCar.ModCar_Name;
                _UnitOfWorkUCService._IModelCarService.Update(_ModelCar);
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
            var _ModelCar = await _UnitOfWorkUCService._IModelCarService.GetByWhere(P => P.ModelCar_ID == ID);
            if (_ModelCar == null) return Ok(new { Message = MessageException.Messages.Null.ToString(), Status = MessageException.Status.Status400 });
            _UnitOfWorkUCService._IModelCarService.Delete(_ModelCar);
            if (await _UnitOfWorkUCService.SaveChange_DataBase_Async() > 0) return Ok(new { Message = MessageException.Messages.Sucess.ToString(), Status = MessageException.Status.Status200 });
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}


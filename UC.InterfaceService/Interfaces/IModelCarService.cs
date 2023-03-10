
using System.Linq.Expressions;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.InterfaceService.Interfaces
{
    public interface IModelCarService : IBaseService<ModelCar>
    {
        Task<IList<DtoModelCar_>> GetAll_SP();
    }
}

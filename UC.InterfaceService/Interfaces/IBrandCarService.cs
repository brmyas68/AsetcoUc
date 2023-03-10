
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.InterfaceService.Interfaces
{
    public interface IBrandCarService : IBaseService<BrandCar>
    {
        Task<IList<DtoBrandModelCar_>> GetBrandModelCar_SP(int BrandType);
    }
}



using UC.ClassDomain.Domains;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface ICityService : IBaseService<City>
    {
        Task<IList<City>> GetAll_By_ProvinceID(int ProvinceID);
    }
}

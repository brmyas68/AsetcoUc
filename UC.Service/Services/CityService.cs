
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly ContextUC _ContextUC;
        public CityService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<City>> GetAll_By_ProvinceID(int ProvinceID)
        {
            if (ProvinceID <= 0) return null;
            return await _ContextUC.City.Where(C => C.City_Province_ID == ProvinceID).ToListAsync().ConfigureAwait(false);
        }
    }
}




using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class ProvinceService : BaseService<Province>, IProvinceService
    {
        private readonly ContextUC _ContextUC;
        public ProvinceService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

    }
}

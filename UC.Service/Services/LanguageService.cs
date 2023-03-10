
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;


namespace UC.Service.Services
{
    public class LanguageService : BaseService<Language>, ILanguageService
    {
        private readonly ContextUC _ContextUC;
        public LanguageService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        
    }
}


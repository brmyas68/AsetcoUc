

using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class LogService : BaseService<Log>, ILogService
    {
        private readonly ContextUC _ContextUC;
        public LogService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

    }
}


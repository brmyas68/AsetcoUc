


using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class TokenService : BaseService<Token>, ITokenService
    {
        private readonly ContextUC _ContextUC;
        public TokenService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<int> GetCount_UserToken(int UserID)
        {
            return await _ContextUC.Token.Where(T => T.Token_UserID == UserID).CountAsync().ConfigureAwait(false);
        }
    }
}




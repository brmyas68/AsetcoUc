

using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface ITokenService : IBaseService<Token>
    {
        Task<int> GetCount_UserToken(int UserID);
    }
}



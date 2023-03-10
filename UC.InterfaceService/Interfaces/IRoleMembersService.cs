

using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IRoleMembersService : IBaseService<RoleMembers>
    {
        Task<IList<RoleMembers_>> GetAllRoleMembers_By_SP(int RoleID, int LangID, int TenantID);
        Task<IList<ListRoleMembers_>> ListRoleMembers_By_SP(int RoleID, int LangID, int TenantID);
    }
}

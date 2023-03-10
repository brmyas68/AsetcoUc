
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<IList<DtoRole_>> GetAllRole_By_SP(int LangID, int TenantID);
        Task<IList<DtoRole_>> GetAllRole_By_SP(int UserID, int LangID, int TenantID);
        Task<IList<DtoSystemRole_>> GetSystemRole_By_SP(int LangID, string SystemTagName);
        Task<bool> IsSystemRole(int User_ID);
        Task<int> GetRoleBy_TagName(string TagName, int TenantID);
    }
}

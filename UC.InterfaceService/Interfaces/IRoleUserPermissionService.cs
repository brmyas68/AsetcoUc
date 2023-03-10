
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IRoleUserPermissionService : IBaseService<RoleUserPermission>
    {
        Task<List<DtoPermissionUserRoutePath>> GetAll_PermissionUserRoutePath(int Language_ID, int UserID_Or_RoleID);
        Task<List<DtoPermissionUserRoleActions>> GetAll_PermissionUserActions(int Language_ID, int UserID_Or_RoleID, int Routstructure_ID);
        Task<List<DtoRouteLink>> GetAllLinkMenuPermission(int UserID, int LangID, int TenantID);
        Task<List<DtoActions>> GetAll_Actions(int UserID, int TenantID, string TagNameForm);
        Task<List<DtoListActions>> GetAll_ListActionsPermission(int UserID, int TenantID);
    }
}



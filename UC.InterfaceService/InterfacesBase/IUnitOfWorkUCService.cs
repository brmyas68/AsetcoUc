


using UC.ClassDomain.Domains;
using UC.Interface.Interfaces;
using UC.InterfaceService.ExternalInterfaces;
using UC.InterfaceService.Interfaces;

namespace UC.InterfaceService.InterfacesBase
{
    public interface IUnitOfWorkUCService : IDisposable
    {
        ICompanyInfoService _ICompanyInfoService { get; }
        ILanguageService _ILanguageService { get; }
        ILogService _ILogService { get; }
        IRoleService _IRoleService { get; }
        IRoleMembersService _IRoleMembersService { get; }
        IRoleUserPermissionService _IRoleUserPermissionService { get; }
        IRouteStructureService _IRouteStructureService { get; }
        ISmsService _ISmsService { get; }
        ITagsknowledgeService _ITagsknowledgeService { get; }
        ITokenService _ITokenService { get; }
        ITranslateTagsService _ITranslateTagsService { get; }
        IUserRoleService _IUserRoleService { get; }
        ICityService _ICityService { get; }
        IProvinceService _IProvinceService { get; }
        IUserService _IUserService { get; }
        IMenuLinkService _IMenuLinkService { get; } 
        IAuthenticationService _IAuthenticationService { get; }
        ISecurityCodeService _ISecurityCodeService { get; }
        IBrandCarService _IBrandCarService { get; }
        IModelCarService _IModelCarService { get; }

        int SaveChange_DataBase();
        Task<int> SaveChange_DataBase_Async();

    }
}

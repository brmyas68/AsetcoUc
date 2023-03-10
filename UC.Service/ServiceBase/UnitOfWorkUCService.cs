

using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.InterfaceService.ExternalInterfaces;
using UC.InterfaceService.Interfaces;
using UC.InterfaceService.InterfacesBase;
using UC.Service.Services;
 

namespace UC.Service.ServiceBase
{
    public class UnitOfWorkUCService : IUnitOfWorkUCService
    {
        private readonly ContextUC _ContextUC;

        private ICompanyInfoService _CompanyInfoService;
        private ILanguageService _LanguageService;
        private ILogService _LogService;
        private IRoleService _RoleService;
        private IRoleMembersService _RoleMembersService;
        private IRoleUserPermissionService _RoleUserPermissionService;
        private IRouteStructureService _RouteStructureService;
        private ISmsService _SmsService;
        private ITagsknowledgeService _TagsknowledgeService;
        private ITokenService _TokenService;
        private ITranslateTagsService _TranslateTagsService;
        private IUserRoleService _UserRoleService;
        private IUserService _UserService;
        private IProvinceService _ProvinceService;
        private ICityService _CityService;
        private UC.Interface.Interfaces.IAuthenticationService _AuthenticationService;
        private ISecurityCodeService _SecurityCodeService;
        private IMenuLinkService _MenuLinkService;
        private IBrandCarService _BrandCarService;
        private IModelCarService _ModelCarService;

        public UnitOfWorkUCService(ContextUC ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public ICompanyInfoService _ICompanyInfoService { get { return _CompanyInfoService = _CompanyInfoService ?? new CompanyInfoService(_ContextUC); } }
        public ILanguageService _ILanguageService { get { return _LanguageService = _LanguageService ?? new LanguageService(_ContextUC); } }
        public ILogService _ILogService { get { return _LogService = _LogService ?? new LogService(_ContextUC); } }
        public IRoleService _IRoleService { get { return _RoleService = _RoleService ?? new RoleService(_ContextUC); } }
        public IRoleMembersService _IRoleMembersService { get { return _RoleMembersService = _RoleMembersService ?? new RoleMembersService(_ContextUC); } }
        public IRoleUserPermissionService _IRoleUserPermissionService { get { return _RoleUserPermissionService = _RoleUserPermissionService ?? new RoleUserPermissionService(_ContextUC); } }
        public IRouteStructureService _IRouteStructureService { get { return _RouteStructureService = _RouteStructureService ?? new RouteStructureService(_ContextUC); } }
        public ISmsService _ISmsService { get { return _SmsService = _SmsService ?? new ExternalServices.SmsService(_ContextUC); } }
        public ITagsknowledgeService _ITagsknowledgeService { get { return _TagsknowledgeService = _TagsknowledgeService ?? new TagsknowledgeService(_ContextUC); } }
        public ITokenService _ITokenService { get { return _TokenService = _TokenService ?? new TokenService(_ContextUC); } }
        public ITranslateTagsService _ITranslateTagsService { get { return _TranslateTagsService = _TranslateTagsService ?? new TranslateTagsService(_ContextUC); } }
        public IUserRoleService _IUserRoleService { get { return _UserRoleService = _UserRoleService ?? new UserRoleService(_ContextUC); } }
        public IUserService _IUserService { get { return _UserService = _UserService ?? new UserService(_ContextUC); } }
        public IProvinceService _IProvinceService { get { return _ProvinceService = _ProvinceService ?? new ProvinceService(_ContextUC); } }
        public ICityService _ICityService { get { return _CityService = _CityService ?? new CityService(_ContextUC); } }
        public UC.Interface.Interfaces.IAuthenticationService _IAuthenticationService { get { return _AuthenticationService = _AuthenticationService ?? new UC.Service.Services.AuthenticationService(_ContextUC); } }
        public ISecurityCodeService _ISecurityCodeService { get { return _SecurityCodeService = _SecurityCodeService ?? new SecurityCodeService(); } }
        public IMenuLinkService _IMenuLinkService { get { return _MenuLinkService = _MenuLinkService ?? new MenuLinkService(_ContextUC); } }
        public IBrandCarService _IBrandCarService { get { return _BrandCarService = _BrandCarService ?? new BrandCarService(_ContextUC); } }
        public IModelCarService _IModelCarService { get { return _ModelCarService = _ModelCarService ?? new ModelCarService(_ContextUC); } }
        
        public int SaveChange_DataBase()
        {
            return _ContextUC.SaveChanges();
        }

        public async Task<int> SaveChange_DataBase_Async()
        {
            return await _ContextUC.SaveChangesAsync(); //.ConfigureAwait(false);
        }

        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _ContextUC.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
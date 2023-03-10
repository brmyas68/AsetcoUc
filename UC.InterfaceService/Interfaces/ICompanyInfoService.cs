
using System.Linq.Expressions;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface ICompanyInfoService : IBaseService<CompanyInfo>
    {
        Task<int> DefaultLanguageCompany(int TenantID);
        public Task<List<DtoCompanyInfo_>> GetAll_SP(int LangID , int AnnexSettingID);
    }
}

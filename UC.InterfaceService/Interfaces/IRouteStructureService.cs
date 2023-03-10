
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IRouteStructureService : IBaseService<RouteStructure>
    {
        Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute_For_Tree(int LanguageID);
        Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRouteAll(int LanguageID);
        Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute(int LanguageID);
        Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute(int LanguageID, int TypeRoute);
        Task<IList<Dto_RouteStructure_>> GetAll_By_Parent_ID(int LanguageID, int ParentID);
        Task<IList<Dto_RouteStructure_>> GetAll_By_Parent_TypeRoute_ID(int LanguageID, int ParentID, int TypeRoute);
        Task<Dto_RouteStructure_> GetRouteStructure_By_ID_SP(int LanguageID, int RouteStructureID);
        Task<IList<Dto_RouteStructure_>> GetAll_Form_RouteStructure(int LangID, int TenantID, string TagNameForm);
        Task<IList<Dto_RouteStructure_>> GetAll_Route_RouteStructureTreeByTagID(int LangID, int TagID, int TenantID, int TypeRout );
    }
}



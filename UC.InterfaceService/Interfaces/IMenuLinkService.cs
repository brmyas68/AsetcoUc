
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.InterfaceService.Interfaces
{
    public interface IMenuLinkService : IBaseService<MenuLink>
    {
        Task<List<DtoMenuLink_>> GetAll_SP(int LangID, int SystemTagID);
        Task<List<DtoMenuLinkGrid_>> GetAll_Grid();
        Task<List<DtoMenuLink_>> GetAllSubMenu_SP(int LangID, int SystemTagID);
    }
}


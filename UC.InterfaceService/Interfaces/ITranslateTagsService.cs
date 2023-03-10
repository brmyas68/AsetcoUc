


using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface ITranslateTagsService : IBaseService<TranslateTags>
    {
        Task<IList<DtoTranslateTags_>> GetAll_By_Language_ID(int LanguageID);
        Task<IList<DtoTranslateTags_>> GetAll_By_Tagsknowledge_ID(int TagsknowledgeID);
        Task<DtoTranslateTags_> GetAll_By_Language_Tagsknowledge_ID(int LanguageID, int TagsknowledgeID);
        Task<List<DtoTranslateTagsLogin>> GetAll_By_Tranlate_Tags_Login(int LanguageID, string Tags_Names);
    }
}



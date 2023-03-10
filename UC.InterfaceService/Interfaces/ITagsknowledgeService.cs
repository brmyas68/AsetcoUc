

using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface ITagsknowledgeService : IBaseService<Tagsknowledge>
    {
        Task<List<Tagsknowledge_>> GetAllRole_SP(int TagType);
        Task<IList<DtoTagsknowledge_>> GetAll_Tags_Translate(int Language_ID,int TagType, int ParentID);

        Task<IList<Tagsknowledge_>> GetAll_List_TagsTranslate(int Language_ID, string TagNameForm);
    }
}



 

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperTranslateTags
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<TranslateTags, DtoTranslateTags>()
                     .ForMember(DtoTL => DtoTL.TranTg_ID, opt => opt.MapFrom(TL => TL.TranslateTags_ID))
                     .ForMember(DtoTL => DtoTL.TranTg_Text, opt => opt.MapFrom(TL => TL.TranslateTags_Text))
                     .ForMember(DtoTL => DtoTL.TranTg_TagID, opt => opt.MapFrom(TL => TL.TranslateTags_TagsknowledgeID))
                     .ForMember(DtoTL => DtoTL.TranTg_LangID, opt => opt.MapFrom(TL => TL.TranslateTags_LanguageID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


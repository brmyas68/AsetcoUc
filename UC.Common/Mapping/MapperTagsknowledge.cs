
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperTagsknowledge
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Tagsknowledge, DtoTagsknowledge>()
                     .ForMember(DtoTG => DtoTG.Tag_ID, opt => opt.MapFrom(TG => TG.Tagsknowledge_ID))
                     .ForMember(DtoTG => DtoTG.Tag_Name, opt => opt.MapFrom(TG => TG.Tagsknowledge_Name))
                     .ForMember(DtoTG => DtoTG.Tag_PID, opt => opt.MapFrom(TG => TG.Tagsknowledge_ParentID))
                     .ForMember(DtoTG => DtoTG.Tag_Type, opt => opt.MapFrom(TG => TG.Tagsknowledge_Type))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


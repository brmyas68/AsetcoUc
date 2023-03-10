
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperLanguage
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Language, DtoLanguage>()
                     .ForMember(DtoL => DtoL.Lang_ID, opt => opt.MapFrom(L => L.Language_ID))
                     .ForMember(DtoL => DtoL.Lang_Name, opt => opt.MapFrom(L => L.Language_Name))
                     .ForMember(DtoL => DtoL.Lang_Rtl, opt => opt.MapFrom(L => L.Language_Rtl))
                     .ForMember(DtoL => DtoL.Lang_Icon, opt => opt.MapFrom(L => L.Language_Icon))
                     .ForMember(DtoL => DtoL.Lang_PreNumber, opt => opt.MapFrom(L => L.Language_PreNumber))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }

    }

}

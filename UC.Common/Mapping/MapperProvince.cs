using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperProvince
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Province, DtoProvince>()
                     .ForMember(DtoP => DtoP.Provi_ID, opt => opt.MapFrom(P => P.Province_ID))
                     .ForMember(DtoP => DtoP.Provi_Name, opt => opt.MapFrom(P => P.Province_Name))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

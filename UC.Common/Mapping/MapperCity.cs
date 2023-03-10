
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperCity
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<City, DtoCity>()
                     .ForMember(DtoC => DtoC.Cty_ID, opt => opt.MapFrom(C => C.City_ID))
                     .ForMember(DtoC => DtoC.Cty_Name, opt => opt.MapFrom(C => C.City_Name))
                     .ForMember(DtoC => DtoC.Cty_Prv_ID, opt => opt.MapFrom(C => C.City_Province_ID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

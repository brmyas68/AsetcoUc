

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperBrandCar
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(B =>
            {
                B.CreateMap<BrandCar, DtoBrandCar>()
                      .ForMember(DtoB => DtoB.BrdCar_ID, opt => opt.MapFrom(B => B.BrandCar_ID))
                      .ForMember(DtoB => DtoB.BrdCar_Name, opt => opt.MapFrom(B => B.BrandCar_Name))
                      .ForMember(DtoB => DtoB.BrdCar_Typ, opt => opt.MapFrom(B => B.BrandCar_Type))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

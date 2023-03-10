using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperModelCar
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(M =>
            {
                M.CreateMap<ModelCar, DtoModelCar>()
                      .ForMember(DtoM => DtoM.ModCar_ID, opt => opt.MapFrom(M => M.ModelCar_ID))
                      .ForMember(DtoM => DtoM.ModCar_Name, opt => opt.MapFrom(M => M.ModelCar_Name))
                      .ForMember(DtoM => DtoM.ModCar_BrdCarID, opt => opt.MapFrom(M => M.ModelCar_BrandCarID))
                      .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

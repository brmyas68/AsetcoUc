
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperLog
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Log, DtoLog>()
                     .ForMember(DtoLg => DtoLg.Log_ID, opt => opt.MapFrom(Lg => Lg.Log_ID))
                     .ForMember(DtoLg => DtoLg.Log_UsrID, opt => opt.MapFrom(Lg => Lg.Log_UserID))
                     .ForMember(DtoLg => DtoLg.Log_RoutStrID, opt => opt.MapFrom(Lg => Lg.Log_RouteStructureID))
                     .ForMember(DtoLg => DtoLg.Log_DateTime, opt => opt.MapFrom(Lg => Lg.Log_DateTime))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

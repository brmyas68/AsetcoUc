
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperRouteStructure
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<RouteStructure, DtoRouteStructure>()
                     .ForMember(DtoRS => DtoRS.RoutStr_ID, opt => opt.MapFrom(RS => RS.RouteStructure_ID))
                     .ForMember(DtoRS => DtoRS.RoutStr_PID, opt => opt.MapFrom(RS => RS.RouteStructure_ParentID))
                     .ForMember(DtoRS => DtoRS.RoutStr_Tag_ID, opt => opt.MapFrom(RS => RS.RouteStructure_Tagsknowledge_ID))
                     .ForMember(DtoRS => DtoRS.RoutStr_TypeRout, opt => opt.MapFrom(RS => RS.RouteStructure_TypeRoute))
                     .ForMember(DtoRS => DtoRS.RoutStr_TnatID, opt => opt.MapFrom(RS => RS.RouteStructure_TenantID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


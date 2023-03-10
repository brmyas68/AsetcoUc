
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperRoleUserPermission
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<RoleUserPermission, DtoRoleUserPermission>()
                     .ForMember(DtoRUP => DtoRUP.RlUsrPer_ID, opt => opt.MapFrom(RUP => RUP.RoleUserPermission_ID))
                     .ForMember(DtoRUP => DtoRUP.RlUsrPer_RolID_UsrID, opt => opt.MapFrom(RUP => RUP.RoleUserPermission_RoleID_UserID))
                     .ForMember(DtoRUP => DtoRUP.RlUsrPer_RoutStrID, opt => opt.MapFrom(RUP => RUP.RoleUserPermission_RouteStructureID))
                     .ForMember(DtoRUP => DtoRUP.RlUsrPer_ParentID, opt => opt.MapFrom(RUP => RUP.RoleUserPermission_ParentID))
                     .ForMember(DtoRUP => DtoRUP.RlUsrPer_BitMrge, opt => opt.MapFrom(RUP => RUP.RoleUserPermission_BitMerge))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


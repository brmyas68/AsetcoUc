

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperRoleMembers
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<RoleMembers, DtoRoleMembers>()
                     .ForMember(DtoRM => DtoRM.RolMbrs_ID, opt => opt.MapFrom(RM => RM.RoleMembers_ID))
                     .ForMember(DtoRM => DtoRM.RolMbrs_RolID, opt => opt.MapFrom(RM => RM.RoleMembers_RoleID))
                     .ForMember(DtoRM => DtoRM.RolMbrs_RolMberID, opt => opt.MapFrom(RM => RM.RoleMembers_RoleMemberID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

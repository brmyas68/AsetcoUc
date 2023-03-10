using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperUserRole
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<UserRole, DtoUserRole>()
                     .ForMember(DtoUR => DtoUR.UsrRol_ID, opt => opt.MapFrom(UR => UR.UserRole_ID))
                     .ForMember(DtoUR => DtoUR.Rol_ID, opt => opt.MapFrom(UR => UR.Role_ID))
                     .ForMember(DtoUR => DtoUR.Usr_ID, opt => opt.MapFrom(UR => UR.User_ID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }

    }
}

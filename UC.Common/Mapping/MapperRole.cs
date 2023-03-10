
using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperRole
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Role, DtoRole>()
                     .ForMember(DtoR => DtoR.Rol_ID, opt => opt.MapFrom(R => R.Role_ID))
                     .ForMember(DtoR => DtoR.Rol_TgID, opt => opt.MapFrom(R => R.Role_TagID))
                     .ForMember(DtoR => DtoR.Rol_TenatID, opt => opt.MapFrom(R => R.Role_TenantID))
                     .ForMember(DtoR => DtoR.Rol_IsAdmin, opt => opt.MapFrom(R => R.Role_IsAdmin))
                     .ForMember(DtoR => DtoR.Rol_IsSysRol, opt => opt.MapFrom(R => R.Role_IsSystemRole))
                     .ForMember(DtoR => DtoR.Rol_RedOnly, opt => opt.MapFrom(R => R.Role_ReadOnly))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}

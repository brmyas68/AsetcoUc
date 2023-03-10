using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperMenuLink
    {

        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<MenuLink, DtoMenuLink>()
                     .ForMember(DtoM => DtoM.MenuLnk_ID, opt => opt.MapFrom(M => M.MenuLink_ID))
                     .ForMember(DtoM => DtoM.MenuLnk_ParntID, opt => opt.MapFrom(M => M.MenuLink_ParentID))
                     .ForMember(DtoM => DtoM.MenuLnk_TypRoutID, opt => opt.MapFrom(M => M.MenuLink_TypeRouteID))
                     .ForMember(DtoM => DtoM.MenuLnk_TagID, opt => opt.MapFrom(M => M.MenuLink_TagID))
                     .ForMember(DtoM => DtoM.MenuLnk_ActnTagID, opt => opt.MapFrom(M => M.MenuLink_ActionTagID))
                     .ForMember(DtoM => DtoM.MenuLnk_FrmTagID, opt => opt.MapFrom(M => M.MenuLink_FormTagID))
                     .ForMember(DtoM => DtoM.MenuLnk_SysTagID, opt => opt.MapFrom(M => M.MenuLink_SystemTagID))
                     .ForMember(DtoM => DtoM.MenuLnk_Icon, opt => opt.MapFrom(M => M.MenuLink_Icon))
                     .ForMember(DtoM => DtoM.MenuLnk_NavigaPath, opt => opt.MapFrom(M => M.MenuLink_NavigationPath))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }

    }
}

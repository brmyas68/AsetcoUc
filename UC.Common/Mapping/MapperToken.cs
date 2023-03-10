

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperToken
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Token, DtoToken>()
                     .ForMember(DtoT => DtoT.Tok_ID, opt => opt.MapFrom(T => T.Token_ID))
                     .ForMember(DtoT => DtoT.Tok_UsrID, opt => opt.MapFrom(T => T.Token_UserID))
                     .ForMember(DtoT => DtoT.Tok_HCode, opt => opt.MapFrom(T => T.Token_HashCode))
                     .ForMember(DtoT => DtoT.Tok_DateC, opt => opt.MapFrom(T => T.Token_DateCreate))
                     .ForMember(DtoT => DtoT.Tok_DateEx, opt => opt.MapFrom(T => T.Token_DateExpire))
                     .ForMember(DtoT => DtoT.Tok_DateLast, opt => opt.MapFrom(T => T.Token_DateLastAccessTime))
                     .ForMember(DtoT => DtoT.Tok_IsA, opt => opt.MapFrom(T => T.Token_IsActive))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


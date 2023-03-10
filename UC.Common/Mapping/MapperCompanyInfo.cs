

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperCompanyInfo
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<CompanyInfo, DtoCompanyInfo>()
                     .ForMember(DtoC => DtoC.CoIn_ID, opt => opt.MapFrom(C => C.CompanyInfo_ID))
                     .ForMember(DtoC => DtoC.CoIn_TagID, opt => opt.MapFrom(C => C.CompanyInfo_TagID))
                     .ForMember(DtoC => DtoC.CoIn_Address, opt => opt.MapFrom(C => C.CompanyInfo_Address))
                     .ForMember(DtoC => DtoC.CoIn_Phone, opt => opt.MapFrom(C => C.CompanyInfo_Phone))
                     .ForMember(DtoC => DtoC.CoIn_Mobile, opt => opt.MapFrom(C => C.CompanyInfo_Mobile))
                     .ForMember(DtoC => DtoC.CoIn_Fax, opt => opt.MapFrom(C => C.CompanyInfo_Fax))
                     .ForMember(DtoC => DtoC.CoIn_Email, opt => opt.MapFrom(C => C.CompanyInfo_Email))
                     .ForMember(DtoC => DtoC.CoIn_Site, opt => opt.MapFrom(C => C.CompanyInfo_Site))
                     .ForMember(DtoC => DtoC.CoIn_Instagram, opt => opt.MapFrom(C => C.CompanyInfo_Instagram))
                     .ForMember(DtoC => DtoC.CoIn_About, opt => opt.MapFrom(C => C.CompanyInfo_About))
                     .ForMember(DtoC => DtoC.CoIn_LangID, opt => opt.MapFrom(C => C.CompanyInfo_LanguageID))
                     .ForMember(DtoC => DtoC.CoIn_SmsNumber, opt => opt.MapFrom(C => C.CompanyInfo_SmsNumber))
                     .ForMember(DtoC => DtoC.CoIn_TypeDateTime, opt => opt.MapFrom(C => C.CompanyInfo_TypeDateTime))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }

    }
}


using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Common.Mapping
{
    public class MapperSms
    {
        public static IMapper MapTo()
        {

            var MappingConfig = new MapperConfiguration(c =>
            {

                c.CreateMap<Sms, DtoSms>()
                     .ForMember(DtoS => DtoS.Sms_ID, opt => opt.MapFrom(S => S.Sms_ID))
                     .ForMember(DtoS => DtoS.Sms_Mobile, opt => opt.MapFrom(S => S.Sms_Mobile))
                     .ForMember(DtoS => DtoS.Sms_ACode, opt => opt.MapFrom(S => S.Sms_ActiveCode))
                     .ForMember(DtoS => DtoS.Sms_Time, opt => opt.MapFrom(S => S.Sms_Time))
                     .ForMember(DtoS => DtoS.Sms_IsA, opt => opt.MapFrom(S => S.Sms_IsActive))
                     .ForMember(DtoS => DtoS.Sms_Status, opt => opt.MapFrom(S => S.Sms_Status))
                     .ForMember(DtoS => DtoS.Sms_MesagID, opt => opt.MapFrom(S => S.Sms_MessageID))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }
    }
}


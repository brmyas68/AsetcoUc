

using AutoMapper;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;


namespace UC.Common.Mapping
{
    public class MapperUser
    {
        public static IMapper MapTo()
        {
            var MappingConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<User, DtoUser>()
                     .ForMember(DtoU => DtoU.Usr_ID, opt => opt.MapFrom(U => U.User_ID))
                      .ForMember(DtoU => DtoU.User_TnatID, opt => opt.MapFrom(U => U.User_TenantID))
                     .ForMember(DtoU => DtoU.Usr_FName, opt => opt.MapFrom(U => U.User_FirstName))
                     .ForMember(DtoU => DtoU.Usr_LName, opt => opt.MapFrom(U => U.User_LastName))
                     .ForMember(DtoU => DtoU.Usr_IdentNum, opt => opt.MapFrom(U => U.User_IdentifyNumber))
                     .ForMember(DtoU => DtoU.Usr_Gender, opt => opt.MapFrom(U => U.User_Gender))
                     .ForMember(DtoU => DtoU.Usr_mail, opt => opt.MapFrom(U => U.User_Email))
                     .ForMember(DtoU => DtoU.Usr_Mobile, opt => opt.MapFrom(U => U.User_Mobile))
                      .ForMember(DtoU => DtoU.Usr_Tell, opt => opt.MapFrom(U => U.User_Tell))
                     .ForMember(DtoU => DtoU.Usr_DateReg, opt => opt.MapFrom(U => U.User_DateRegister))
                     .ForMember(DtoU => DtoU.Usr_UName, opt => opt.MapFrom(U => U.User_UserName))
                     .ForMember(DtoU => DtoU.Usr_HPass, opt => opt.MapFrom(U => U.User_HashPassword))
                     .ForMember(DtoU => DtoU.Usr_Prov_ID, opt => opt.MapFrom(U => U.User_Province_ID))
                     .ForMember(DtoU => DtoU.Usr_Cty_ID, opt => opt.MapFrom(U => U.User_City_ID))
                     .ForMember(DtoU => DtoU.Usr_Address, opt => opt.MapFrom(U => U.User_Address))
                     .ForMember(DtoU => DtoU.Usr_IsA, opt => opt.MapFrom(U => U.User_IsActive))
                  //   .ForMember(DtoU => DtoU.Usr_IsFul, opt => opt.MapFrom(U => U.User_IsFullData))
                  //   .ForMember(DtoU => DtoU.Usr_IsChek, opt => opt.MapFrom(U => U.User_IsChecked))
                     .ForMember(DtoU => DtoU.Usr_IsBlk, opt => opt.MapFrom(U => U.User_IsBlock))
                     .ForMember(DtoU => DtoU.Usr_Desc, opt => opt.MapFrom(U => U.User_Description))
                     .ForMember(DtoU => DtoU.Usr_ShabaNum, opt => opt.MapFrom(U => U.User_ShabaNumber))
                     .ForMember(DtoU => DtoU.Usr_PostCode, opt => opt.MapFrom(U => U.User_PostalCode))
                     .ReverseMap();
            });

            return MappingConfig.CreateMapper();
        }


    }
}


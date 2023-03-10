

namespace UC.ClassDTO.DTOs
{
    public class DtoUser
    {
        public int Usr_ID { get; set; }
        public int User_TnatID { get; set; } 
        public string Usr_FName { get; set; }
        public string Usr_LName { get; set; }
        public string Usr_IdentNum { get; set; }
        public int Usr_Gender { get; set; }
        public string Usr_mail { get; set; }
        public string Usr_Mobile { get; set; }
        public string Usr_Tell { get; set; } 
        public DateTime Usr_DateReg { get; set; }
        public string Usr_UName { get; set; }
        public string Usr_HPass { get; set; }
        public string Usr_Img { get; set; }
        public int Usr_Prov_ID { get; set; }
        public int Usr_Cty_ID { get; set; }
        public string Usr_Address { get; set; }
        public bool Usr_IsA { get; set; }
       // public bool Usr_IsFul { get; set; }
       // public bool Usr_IsChek { get; set; }
        public bool Usr_IsBlk { get; set; }
        public string Usr_Desc { get; set; }
        public string Usr_ShabaNum { get; set; }
        public string Usr_PostCode { get; set; }
    }
}

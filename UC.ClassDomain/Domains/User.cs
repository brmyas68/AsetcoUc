

namespace UC.ClassDomain.Domains
{
    public class User
    {
        public User()
        {
            //this.UserRoles = new HashSet<UserRole>();
            //this.RoleUserPermissions = new HashSet<RoleUserPermission>();
            //this.Tokens = new HashSet<Token>();
            //this.Log = new HashSet<Log>();
        }

        public int User_ID { get; set; }
        public int User_TenantID { get; set; }
        public string User_FirstName { get; set; }
        public string User_LastName { get; set; }
        public string User_IdentifyNumber { get; set; }
        public int User_Gender { get; set; }
        public string User_Email { get; set; }
        public string User_Mobile { get; set; }
        public string User_Tell { get; set; }
        public DateTime User_DateRegister { get; set; }
        public string User_UserName { get; set; }
        public string User_HashPassword { get; set; }
        public int User_Province_ID { get; set; }
        public int User_City_ID { get; set; }
        public string User_Address { get; set; }
        public bool User_IsActive { get; set; }
      //  public bool User_IsFullData { get; set; }
      //  public bool User_IsChecked { get; set; }
        public bool User_IsBlock { get; set; }
        public string User_Description { get; set; }
        public string User_ShabaNumber { get; set; }
        public string User_PostalCode { get; set; } 

        //public virtual ICollection<UserRole> UserRoles { get; set; }
        //public virtual ICollection<RoleUserPermission> RoleUserPermissions { get; set; }
        //public virtual ICollection<Token> Tokens { get; set; }
        //public virtual ICollection<Log> Log { get; set; }
        //public virtual Province Province { get; set; }
        //public virtual City City { get; set; } 
    }
}

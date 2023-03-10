

namespace UC.ClassDomain.Domains
{
    public class Role
    {
        public Role()
        {
            //  this.UserRoles = new HashSet<UserRole>();
            //   this.RoleUserPermissions = new HashSet<RoleUserPermission>();

        }

        public int Role_ID { get; set; }
        public int Role_TagID { get; set; }
        public int Role_TenantID { get; set; }
        public bool Role_IsAdmin { get; set; }
        public bool Role_IsSystemRole { get; set; } 
        public bool Role_ReadOnly { get; set; } 

        //   public virtual ICollection<UserRole> UserRoles { get; set; }
        //   public virtual ICollection<RoleUserPermission> RoleUserPermissions { get; set; }

    }
}

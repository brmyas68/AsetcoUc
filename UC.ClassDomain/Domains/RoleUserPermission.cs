

namespace UC.ClassDomain.Domains
{
    public class RoleUserPermission
    {
        public int RoleUserPermission_ID { get; set; }
        public int RoleUserPermission_RoleID_UserID { get; set; }
        public int RoleUserPermission_RouteStructureID { get; set; }
        public int RoleUserPermission_ParentID { get; set; }
        public bool? RoleUserPermission_BitMerge { get; set; }



     //   public virtual User User { get; set; }
      //  public virtual Role Role { get; set; }
      //  public virtual Tagsknowledge Tagsknowledge { get; set; }

    }
}

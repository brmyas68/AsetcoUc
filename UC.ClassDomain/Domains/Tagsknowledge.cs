

namespace UC.ClassDomain.Domains
{
    public class Tagsknowledge
    {
        public Tagsknowledge()
        {
            // this.TranslateTags = new HashSet<TranslateTags>();
            // this.RoleUserPermission = new HashSet<RoleUserPermission>();
            //  this.RouteStructure = new HashSet<RouteStructure>();
        }

        public int Tagsknowledge_ID { get; set; }
        public string Tagsknowledge_Name { get; set; }
        public int Tagsknowledge_ParentID { get; set; }
        public TagType Tagsknowledge_Type { get; set; }

        //  public virtual ICollection<TranslateTags> TranslateTags { get; set; }
        //  public virtual ICollection<RoleUserPermission> RoleUserPermission { get; set; }
        //  public virtual ICollection<RouteStructure> RouteStructure { get; set; }

    }

    public enum TagType
    {
        Route,
        Form,
        Action,
        Titel,
        Alert,
        Role,
        Archive,
        Logo,
        System,
        LinkMenu, 
        Location,
    }
}

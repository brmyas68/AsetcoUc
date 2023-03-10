

namespace UC.ClassDomain.Domains
{
    public class RouteStructure
    {
        public RouteStructure()
        {
            //  this.Log = new HashSet<Log>();
        }
        public int RouteStructure_ID { get; set; }
        public int RouteStructure_TenantID { get; set; }
        public int RouteStructure_ParentID { get; set; }
        public int RouteStructure_Tagsknowledge_ID { get; set; }
        public int RouteStructure_TypeRoute { get; set; }

        //   public virtual Tagsknowledge Tagsknowledge { get; set; }
        //   public virtual ICollection<Log> Log { get; set; }
    }
}



namespace UC.ClassDomain.Domains
{
    public class City
    {
        public City()
        {
           // this.User = new HashSet<User>();
        }
        public int City_ID { get; set; }
        public string City_Name { get; set; }
        public int City_Province_ID { get; set; }

        //public virtual Province Province { get; set; }
        //public virtual ICollection<User> User { get; set; }
    }
}

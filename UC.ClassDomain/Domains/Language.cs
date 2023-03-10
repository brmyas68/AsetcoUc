

namespace UC.ClassDomain.Domains
{
    public class Language
    {
        public Language()
        {
          //  this.TranslateTags = new HashSet<TranslateTags>();
        }

        public int Language_ID { get; set; }
        public string Language_Name { get; set; }
        public bool? Language_Rtl { get; set; }
        public string Language_Icon { get; set; }
        public string Language_PreNumber { get; set; } 
       

      //  public virtual CompanyInfo CompanyInfo { get; set; }
       // public virtual ICollection<TranslateTags> TranslateTags { get; set; }
    }
}

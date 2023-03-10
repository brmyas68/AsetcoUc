

namespace UC.ClassDomain.Domains
{
    public class CompanyInfo
    {
        public CompanyInfo()
        {
           // this.ServerConnection = new HashSet<ServerConnection>();
        }

        public int CompanyInfo_ID { get; set; }
        public int CompanyInfo_TagID { get; set; }
        public string CompanyInfo_Address { get; set; }
        public string CompanyInfo_Phone { get; set; }
        public string CompanyInfo_Mobile { get; set; }
        public string CompanyInfo_Fax { get; set; }
        public string CompanyInfo_Email { get; set; }
        public string CompanyInfo_Site { get; set; }
        public string CompanyInfo_Instagram { get; set; }
        public string CompanyInfo_About { get; set; }
        public string CompanyInfo_SmsNumber { get; set; }
        public int CompanyInfo_LanguageID { get; set; }
        public int CompanyInfo_TypeDateTime { get; set; }

       // public virtual Language Language { get; set; }
      //  public virtual ICollection<ServerConnection> ServerConnection { get; set; }


    }
}

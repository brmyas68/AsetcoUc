

namespace UC.ClassDomain.Domains
{
    public class Sms
    {
        public int Sms_ID { get; set; }
        public string Sms_Mobile { get; set; }
        public string Sms_ActiveCode { get; set; }
        public DateTime Sms_Time { get; set; }
        public bool Sms_IsActive { get; set; }
        public int Sms_Status { get; set; }
        public string Sms_MessageID { get; set; } 
    }
}

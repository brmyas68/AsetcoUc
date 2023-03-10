

namespace UC.ClassDTO.DTOs
{
    public class DtoSettingToken
    {
        public string Signing_Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public double Expir_Minutes { get; set; }
        public Guid Guid { get; set; }
        public DateTime DateTime_UtcNow { get; set; }
        public DateTime DateTime_Now { get; set; } 

    }
}

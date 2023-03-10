

using Microsoft.EntityFrameworkCore;

namespace UC.ClassDomain.Domains
{
    public class Token
    {
        public int Token_ID { get; set; }
        public int Token_UserID { get; set; }
        public string Token_HashCode { get; set; }
        public DateTime Token_DateCreate { get; set; }
        public DateTime Token_DateExpire { get; set; }
        public DateTime Token_DateLastAccessTime { get; set; }
        public bool Token_IsActive { get; set; }

     //   public virtual User User { get; set; }

    }
}

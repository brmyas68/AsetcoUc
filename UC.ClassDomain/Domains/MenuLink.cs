
namespace UC.ClassDomain.Domains
{
    public class MenuLink
    {
        public int MenuLink_ID { get; set; }
        public int MenuLink_TagID { get; set; }
        public int MenuLink_FormTagID { get; set; }
        public int MenuLink_ActionTagID { get; set; }
        public int MenuLink_SystemTagID { get; set; }
        public int MenuLink_TypeRouteID { get; set; }
        public int MenuLink_ParentID { get; set; }
        public string MenuLink_Icon { get; set; }
        public string MenuLink_NavigationPath { get; set; }  
    }
}

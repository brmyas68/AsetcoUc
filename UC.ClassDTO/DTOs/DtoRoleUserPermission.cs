 

namespace UC.ClassDTO.DTOs
{
    public class DtoRoleUserPermission
    {
        public int RlUsrPer_ID { get; set; }
        public int RlUsrPer_RolID_UsrID { get; set; }
        public int RlUsrPer_RoutStrID { get; set; }
        public int RlUsrPer_ParentID { get; set; }
        public bool? RlUsrPer_BitMrge { get; set; }
 
    }
}

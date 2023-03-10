
 

namespace UC.ClassDTO.DTOs.Custom
{
    public class  DtoUserObject
    {
       
        public DtoUser DtoUser { get; set; }
        public List<int> RolesID { get; set; }
        public List<DtoRoleUserPermission> RolUsrPerm { get; set; }
    }
}



namespace UC.Common.Enum
{
    public static class EnumPermission
    {
        public enum Role
        {
            Role_UC_God,
            
        };

        public enum Controllers
        {
            Form_UC_Role,
            Form_UC_User,
            Form_UC_UserRole,
            Form_UC_RoleUserPermission,
            Form_UC_RouteStructure,

        };

        public enum Actions
        {
            Action_UC_User_GetAll,
            Action_UC_User_GetByID,
            Action_UC_User_Insert,
            Action_UC_User_Update,
            Action_UC_User_Delete,
            Action_UC_User_ISActive,
            Action_UC_User_ISBlock,
            Action_UC_User_UploadImage,
            Action_UC_User_DeleteImage,
            Action_UC_UserRole_Insert,
            Action_UC_UserRole_Delete,
            Action_UC_RoleUserPermission_GetAll,
            Action_UC_RoleUserPermission_Insert,
            Action_UC_RoleUserPermission_Delete,
            Action_UC_Role_GetAll,
        }

    }
}

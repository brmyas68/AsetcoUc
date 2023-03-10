
namespace UC.Common.StoredProcedure
{
    public static class SPUC
    {
        public enum SPName
        {
            UC_CheckPermissions,
            UC_GetAllCompany,
            UC_GetRoleNamesByUserID, UC_GetSystemSubRoles, UC_GetRoleByTagName,
            UC_CheckUserIsSystemRole,
            UC_GetAllActions, UC_GetRouteActions, UC_GetRouteString, UC_ListActionsPermission,
            UC_GetRouteStructureByRouteID, UC_GetAllRouteStructureForTree, UC_GetAllRouteStructureByTypeRouteAll, UC_GetAllRouteStructureByTypeRoute, UC_GetRouteStructureByParentID, UC_GetRouteStructureByParentTypeRoute, UC_GetAllFormRouteStructure, UC_GetAllRouteStructurTreeByTagID,
            UC_GetAllTagsTranslate, UC_TagsTranslate,
            UC_GetTranslateTagsByLangID, UC_GetTranslateTagsByLangTagID, UC_GetTranslateTagsByTagID, UC_LoginTranslateTags,
            UC_GetRoleAllByUserID, UC_GetAllRole, UC_GetAllRoleMemberByUserID, UC_GetAllRoleTranslate,
            UC_GetAllRoleMemberTranslate, UC_ListRoleMemberTranslate,
            UC_GetUserByID, UC_GetUserAll_Grid, UC_GetAllUsersByRoleTagName, Uc_GetUsers, UC_CheckUserName,
            UC_GetAllMenuLinkTranslate, UC_MenuLinkTransTagName, UC_CreateLinkMenu, UC_GetAllSubMenuLink,
            UC_GetAllBrandCar, UC_GetAllModelCar,
        };


        public static string SP_FindToken
        {
            get { return "EXEC UC_FindToken  @Token_UserID,@Token_HashCode"; }
        }
        public static string SP_GetRouteStructureByTypeRoute
        {
            get { return "EXEC UC_GetRouteStructureByTypeRoute @LanguageID,@RouteStructure_TypeRoute"; }

        }

    }
}





using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<DtoUser_> GetUser_By_ID_SP(int UserID);
        Task<List<DtoUser_>> GetAllUser_SP(int AnnexSetting_ID);
        Task<List<DtoUser_>> GetAllUser_SP(DataTable Dt, int LanguageID, int UserID, int TenantID);

        Task<List<DtoUser_Role>> GetAllUsersByRoleTagName_SP(string RoleTagName, int TenantID);

        Task<bool> IsActiveUser(int User_ID, bool State);
        Task<bool> IsBlockUser(int User_ID, bool State);
        Task<bool> CheckUserName(int TenantID, string User_Name);
        Task<bool> CheckUserMobile(int TenantID, string User_Mobile);
        Task<User> CheckUserMobileCLS(int TenantID, string User_Mobile);
        Task<bool> UpdatePassword(int User_ID, String NewPassword);
        string CreatePasswordTemporary();

    }
}




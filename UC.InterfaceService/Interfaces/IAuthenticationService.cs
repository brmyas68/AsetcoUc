



using System;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;

namespace UC.Interface.Interfaces
{
    public interface IAuthenticationService
    {
        Task<int> CountUser(int User_ID);

        Task<bool> IsValidToken(int User_ID, string Token_Hash);
        Task<bool> IsValidAuthoriz(int User_ID, string Role_Name);

        Task<string> CreateToken(int User_ID, DtoSettingToken DtoSettingToken);

        string CreateHashing(string Value);

        string EnCryptASE(string key, string text);
        string DeCryptASE(string key, string value);

        Task<int> CheckPermissions(int User_ID, int Tenant_ID, string TagName_Form, string TagName_Action);
    }
}





using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.InterfaceService.InterfacesBase;

namespace UC.Interface.Interfaces
{
    public interface IUserRoleService : IBaseService<UserRole>
    {
        Task<List<DtoRole_>> GetAllRole_By_UserID_SP(int User_ID, int Language_ID);
        Task<bool> CheckUserRole(int User_ID, int Role_ID);
    }
}


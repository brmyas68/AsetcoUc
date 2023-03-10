


using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;
using UC.Common.StoredProcedure;

namespace UC.Service.Services
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private readonly ContextUC _ContextUC;
        public UserRoleService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<bool> CheckUserRole(int User_ID, int Role_ID)
        {
            bool exits = await _ContextUC.UserRole.AnyAsync(UR => UR.User_ID == User_ID && UR.Role_ID == Role_ID);
            return exits;
        }
        public async Task<List<UserRole>> GetAllRole_By_UserID(int User_ID)
        {
            var _UserRoles = await _ContextUC.UserRole.Where(x => x.User_ID == User_ID).ToListAsync().ConfigureAwait(false);
            return _UserRoles;
        }


        public async Task<List<DtoRole_>> GetAllRole_By_UserID_SP(int User_ID, int Language_ID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRoleAllByUserID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", Language_ID));
            _Cmd.Parameters.Add(new SqlParameter("UserID", User_ID));
            var _DtoRole = new List<DtoRole_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoRole.Add(
                            new DtoRole_
                            {
                                Rol_ID = Convert.ToInt32(_Reader["Rol_ID"]),
                                Rol_TagID = Convert.ToInt32(_Reader["Rol_TagID"]),
                                TransTagText = Convert.ToString(_Reader["TransTagText"]),
                            });
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _DtoRole;
        }
    }
}


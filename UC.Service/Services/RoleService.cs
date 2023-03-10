

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;
 

namespace UC.Service.Services
{
    public class RoleService : BaseService<Role>,  IRoleService
    {
        private readonly ContextUC _ContextUC;
        public RoleService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<DtoRole_>> GetAllRole_By_SP(int LangID, int TenantID)
        {

            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRoleTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
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
                                Rol_ID = Convert.ToInt32(_Reader["Role_ID"]),
                                Rol_TagID = Convert.ToInt32(_Reader["Tag_ID"]),
                                // Rol_MemberID = Convert.ToInt32(_Reader["Role_MemberID"]),
                                // Rol_TagName = Convert.ToString(_Reader["Tag_Name"]),
                                TransTagText = Convert.ToString(_Reader["Tag_TransText"]),
                                Rol_IsSystemRole = Convert.ToBoolean(_Reader["IsSystemRole"]),
                                Rol_ReadOnly = Convert.ToBoolean(_Reader["ReadOnlyRole"]),
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

        public async Task<IList<DtoRole_>> GetAllRole_By_SP(int UserID, int LangID, int TenantID)
        {

            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRoleMemberByUserID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("UserID", UserID));
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
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
                                Rol_ID = Convert.ToInt32(_Reader["Role_ID"]),
                                Rol_TagID = Convert.ToInt32(_Reader["Tag_ID"]),
                                Rol_MemberID = Convert.ToInt32(_Reader["Role_MemberID"]),
                                Rol_TagName = Convert.ToString(_Reader["Tag_Name"]),
                                TransTagText = Convert.ToString(_Reader["Tag_TransText"]),
                                Rol_IsSystemRole = Convert.ToBoolean(_Reader["IsSystemRole"]),
                                Rol_ReadOnly = Convert.ToBoolean(_Reader["ReadOnlyRole"]),
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

        public async Task<bool> IsSystemRole(int User_ID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_CheckUserIsSystemRole.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("UserID", User_ID));
            var _IsSystemRole = false;
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _IsSystemRole = Convert.ToBoolean(_Reader["IsSystemRole"]);

                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _IsSystemRole;
        }

        public async Task<IList<DtoSystemRole_>> GetSystemRole_By_SP(int LangID, string SystemTagName)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetSystemSubRoles.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("Lang_ID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("ParentSystemTag", SystemTagName));
            var _DtoSystemRole = new List<DtoSystemRole_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoSystemRole.Add(
                            new DtoSystemRole_
                            {
                                Rol_ID = Convert.ToInt32(_Reader["Role_ID"]),
                                Rol_TagName = Convert.ToString(_Reader["Tagsknowledge_Name"]),
                                TransTagText = Convert.ToString(_Reader["Tag_TransText"]),
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
            return _DtoSystemRole;
        }

        public async Task<int> GetRoleBy_TagName(string TagName, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRoleByTagName.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TagName", TagName));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
            var _RoleID = -1;
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RoleID = Convert.ToInt32(_Reader["Role_ID"]);

                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _RoleID;
        }
    }
}


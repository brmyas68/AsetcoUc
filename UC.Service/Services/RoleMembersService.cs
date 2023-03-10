


using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class RoleMembersService : BaseService<RoleMembers>, IRoleMembersService
    {
        private readonly ContextUC _ContextUC;
        public RoleMembersService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<ListRoleMembers_>> ListRoleMembers_By_SP(int RoleID, int LangID, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_ListRoleMemberTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
            _Cmd.Parameters.Add(new SqlParameter("RoleID", RoleID));
            var _RoleMembers = new List<ListRoleMembers_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RoleMembers.Add(
                            new ListRoleMembers_
                            {
                                 Role_ID = Convert.ToInt32(_Reader["Role_ID"]),
                                 RoleMembers_TransTagName = _Reader["Trans_TagName"].ToString(),
                                  
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
            return _RoleMembers;
        }

        public async Task<IList<RoleMembers_>> GetAllRoleMembers_By_SP(int RoleID, int LangID, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRoleMemberTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
            _Cmd.Parameters.Add(new SqlParameter("RoleID", RoleID));
            var _RoleMembers = new List<RoleMembers_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RoleMembers.Add(
                            new RoleMembers_
                            {
                                RoleMembers_ID = Convert.ToInt32(_Reader["Member_ID"]),
                                RoleMembers_TransTagName = _Reader["Trans_TagName"].ToString(),
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
            return _RoleMembers;
        }
    }
}

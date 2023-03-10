


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
    public class RoleUserPermissionService : BaseService<RoleUserPermission>, IRoleUserPermissionService
    {
        private readonly ContextUC _ContextUC;
        public RoleUserPermissionService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }



        public async Task<List<DtoRouteLink>> GetAllLinkMenuPermission(int UserID, int LangID, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_CreateLinkMenu.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("User_ID", UserID));
            _Cmd.Parameters.Add(new SqlParameter("Lang_ID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("Tenant_ID", TenantID));
            var _DtoRouteLink = new List<DtoRouteLink>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoRouteLink.Add(
                            new DtoRouteLink
                            {
                                ActionName = Convert.ToString(_Reader["ActionName"]),
                                Action_TagID = Convert.ToInt32(_Reader["MenuLink_ActionTagID"]),
                                Action_TagName = Convert.ToString(_Reader["Action_TagName"]),
                                FormName = _Reader["FormName"].ToString(),
                                Form_TagID = Convert.ToInt32(_Reader["MenuLink_FormTagID"]),
                                Form_TagName = Convert.ToString(_Reader["Form_TagName"]),
                                Parent_ID = Convert.ToInt32(_Reader["MenuLink_ParentID"]),
                                System_TagName = Convert.ToString(_Reader["System_TagName"]),
                                System_TagID = Convert.ToInt32(_Reader["MenuLink_SystemTagID"]),
                                Trans_Link = Convert.ToString(_Reader["Trans_Link"]),
                                TypeRoute = Convert.ToInt32(_Reader["MenuLink_TypeRouteID"]),
                                Link_Icon = Convert.ToString(_Reader["MenuLink_Icon"]),
                                Navigat_Path = Convert.ToString(_Reader["MenuLink_NavigationPath"]),
                                Link_ID = Convert.ToString(_Reader["MenuLink_ID"]),

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
            return _DtoRouteLink;
        }

        public async Task<List<DtoActions>> GetAll_Actions(int UserID, int TenantID, string TagNameForm)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllActions.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("UserID", UserID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
            _Cmd.Parameters.Add(new SqlParameter("TagNameForm", TagNameForm));
            var _DtoActions = new List<DtoActions>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoActions.Add(
                            new DtoActions
                            {
                                Actions_TagID = Convert.ToInt32(_Reader["TagID"]),
                                Actions_TagName = _Reader["TagName"].ToString(),

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
            return _DtoActions;
        }

        public async Task<List<DtoListActions>> GetAll_ListActionsPermission(int UserID, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_ListActionsPermission.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("User_ID", UserID));
            _Cmd.Parameters.Add(new SqlParameter("Tenant_ID", TenantID));
            var _DtoListActions = new List<DtoListActions>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoListActions.Add(
                            new DtoListActions
                            {
                                Action_TagID = Convert.ToInt32(_Reader["ActionTagID"]),
                                Action_TagName = Convert.ToString(_Reader["ActionTagName"]),

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
            return _DtoListActions;
        }

        public async Task<List<DtoPermissionUserRoleActions>> GetAll_PermissionUserActions(int Language_ID, int UserID_Or_RoleID, int Routstructure_ID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRouteActions.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("Language_ID", Language_ID));
            _Cmd.Parameters.Add(new SqlParameter("UserRole_ID", UserID_Or_RoleID));
            _Cmd.Parameters.Add(new SqlParameter("Routstructure_ID", Routstructure_ID));
            var _DtoPermissionUserRoleActions = new List<DtoPermissionUserRoleActions>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoPermissionUserRoleActions.Add(
                            new DtoPermissionUserRoleActions
                            {
                                Routstructure_ID = Convert.ToInt32(_Reader["RouteStructure_ID"]),
                                Tag_Name = Convert.ToString(_Reader["Tag_Name"]),
                                Action_State = Convert.ToBoolean(_Reader["Action_State"]),

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
            return _DtoPermissionUserRoleActions;
        }

        public async Task<List<DtoPermissionUserRoutePath>> GetAll_PermissionUserRoutePath(int Language_ID, int UserID_Or_RoleID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRouteString.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("Language_ID", Language_ID));
            _Cmd.Parameters.Add(new SqlParameter("UserRole_ID", UserID_Or_RoleID));
            var _DtoPermissionUserRoutePath = new List<DtoPermissionUserRoutePath>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoPermissionUserRoutePath.Add(
                            new DtoPermissionUserRoutePath
                            {
                                RouteStructure_ID = Convert.ToInt32(_Reader["RouteStructure_ID"]),
                                RouteStructure_ParentID = Convert.ToInt32(_Reader["RouteStructure_ParentID"]),
                                Route_Path = Convert.ToString(_Reader["Route_Path"]),

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
            return _DtoPermissionUserRoutePath;
        }
    }
}
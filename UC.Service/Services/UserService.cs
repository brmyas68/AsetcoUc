
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly ContextUC _ContextUC;
        public UserService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<bool> CheckUserMobile(int TenantID, string User_Mobile)
        {
            var UserID = (await _ContextUC.User.FirstOrDefaultAsync(x => x.User_TenantID == TenantID && x.User_Mobile == User_Mobile).ConfigureAwait(false))?.User_ID;
            if (UserID ==  null) return false;
            var RoleID = (await _ContextUC.UserRole.FirstOrDefaultAsync(x => x.User_ID == UserID).ConfigureAwait(false))?.Role_ID;
            if (RoleID == null ) return false;
            var IsSystem = (await _ContextUC.Role.FirstOrDefaultAsync(x => x.Role_ID == RoleID).ConfigureAwait(false)).Role_IsSystemRole;
            if (!IsSystem) return false;
            return true;
        }

        public async Task<User> CheckUserMobileCLS(int TenantID, string User_Mobile)
        {
            return await _ContextUC.User.FirstOrDefaultAsync(x => x.User_TenantID == TenantID && x.User_Mobile == User_Mobile).ConfigureAwait(false);
        }

        public async Task<bool> CheckUserName(int TenantID, string User_Name)
        {
            var CountUser = 0;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_CheckUserName.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter_UserName = new SqlParameter();
            Parameter_UserName.ParameterName = "UserName";
            Parameter_UserName.SqlDbType = SqlDbType.NVarChar;
            Parameter_UserName.Value = User_Name;
            _Cmd.Parameters.Add(Parameter_UserName);

            SqlParameter Parameter_TenantID = new SqlParameter();
            Parameter_TenantID.ParameterName = "TenantID";
            Parameter_TenantID.SqlDbType = SqlDbType.Int;
            Parameter_TenantID.Value = TenantID;
            _Cmd.Parameters.Add(Parameter_TenantID);

            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        CountUser = Convert.ToInt32(_Reader["countfound"]);
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            if (CountUser > 0) return true;
            return false;
        }

        public string CreatePasswordTemporary()
        {
            Random _random = new Random();

            int Num1 = _random.Next(1, 9);
            int Num2 = _random.Next(10, 99);


            char offsetL = 'a';
            char offsetU = 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            var char1 = (char)_random.Next(offsetL, offsetL + lettersOffset);
            var char2 = (char)_random.Next(offsetU, offsetU + lettersOffset);

            return Num1.ToString() + "" + char1.ToString() + "" + Num2.ToString() + "" + char2.ToString();
        }

        public async Task<List<DtoUser_Role>> GetAllUsersByRoleTagName_SP(string RoleTagName, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllUsersByRoleTagName.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;


            SqlParameter Parameter_RoleTagName = new SqlParameter();
            Parameter_RoleTagName.ParameterName = "RoleTagName";
            Parameter_RoleTagName.SqlDbType = SqlDbType.NVarChar;
            Parameter_RoleTagName.Value = RoleTagName;
            _Cmd.Parameters.Add(Parameter_RoleTagName);

            SqlParameter Parameter_Tenant = new SqlParameter();
            Parameter_Tenant.ParameterName = "TenantID";
            Parameter_Tenant.SqlDbType = SqlDbType.Int;
            Parameter_Tenant.Value = TenantID;
            _Cmd.Parameters.Add(Parameter_Tenant);

            var _ListUser_Agent = new List<DtoUser_Role>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _ListUser_Agent.Add(
                            new DtoUser_Role
                            {
                                User_ID = Convert.ToInt32(_Reader["User_ID"]),
                                User_FullName = _Reader["User_FullName"].ToString(),
                            }); ;
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _ListUser_Agent;
        }

        public async Task<List<DtoUser_>> GetAllUser_SP(int AnnexSetting_ID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetUserAll_Grid.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter Parameter_AnnexSetting = new SqlParameter();
            Parameter_AnnexSetting.ParameterName = "AnexSetingID";
            Parameter_AnnexSetting.SqlDbType = SqlDbType.Int;
            Parameter_AnnexSetting.Value = AnnexSetting_ID;
            _Cmd.Parameters.Add(Parameter_AnnexSetting);
            var _UserList = new List<DtoUser_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _UserList.Add(
                            new DtoUser_
                            {
                                Usr_ID = Convert.ToInt32(_Reader["Usr_ID"]),
                                Usr_TnatID = Convert.ToInt32(_Reader["Usr_TnatID"]),
                                Usr_FName = Convert.ToString(_Reader["Usr_FName"]),
                                Usr_LName = Convert.ToString(_Reader["Usr_LName"]),
                                Usr_IdentNum = Convert.ToString(_Reader["Usr_IdentNum"]),
                                Usr_Gender = Convert.ToInt16(_Reader["Usr_Gender"]),
                                Usr_Mail = Convert.ToString(_Reader["Usr_Mail"]),
                                Usr_Mobile = Convert.ToString(_Reader["Usr_Mobile"]),
                                Usr_DateReg = Convert.ToString(_Reader["Usr_DateReg"]),
                                Usr_UName = Convert.ToString(_Reader["Usr_UName"]),
                                Usr_ProvID = Convert.ToInt32(_Reader["Usr_ProvID"]),
                                Usr_CtyID = Convert.ToInt32(_Reader["Usr_CtyID"]),
                                Usr_ProvName = Convert.ToString(_Reader["Usr_ProvName"]),
                                Usr_CtyName = Convert.ToString(_Reader["Usr_CtyName"]),
                                Usr_Address = Convert.ToString(_Reader["Usr_Address"]),
                                Usr_IsA = Convert.ToBoolean(_Reader["Usr_IsA"]),
                                Usr_IsBlk = Convert.ToBoolean(_Reader["Usr_IsBlk"]),
                                Usr_Img = _Reader["Path_Image"].ToString(),
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
            return _UserList;
        }

        public async Task<List<DtoUser_>> GetAllUser_SP(DataTable Dt, int LanguageID, int UserID, int TenantID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.Uc_GetUsers.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "Details";
            Parameter.SqlDbType = SqlDbType.Structured;
            Parameter.Value = Dt;
            _Cmd.Parameters.Add(Parameter);

            SqlParameter Parameter_Lang = new SqlParameter();
            Parameter_Lang.ParameterName = "LanguageID";
            Parameter_Lang.SqlDbType = SqlDbType.Int;
            Parameter_Lang.Value = LanguageID;
            _Cmd.Parameters.Add(Parameter_Lang);

            SqlParameter Parameter_UserID = new SqlParameter();
            Parameter_UserID.ParameterName = "User_id";
            Parameter_UserID.SqlDbType = SqlDbType.Int;
            Parameter_UserID.Value = UserID;
            _Cmd.Parameters.Add(Parameter_UserID);


            SqlParameter Parameter_Tenant = new SqlParameter();
            Parameter_Tenant.ParameterName = "TenantID";
            Parameter_Tenant.SqlDbType = SqlDbType.Int;
            Parameter_Tenant.Value = TenantID;
            _Cmd.Parameters.Add(Parameter_Tenant);

            var _UserList = new List<DtoUser_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _UserList.Add(
                            new DtoUser_
                            {
                                Usr_ID = Convert.ToInt32(_Reader["Usr_ID"]),
                                Usr_TnatID = Convert.ToInt32(_Reader["Usr_TnatID"]),
                                Usr_FName = Convert.ToString(_Reader["Usr_FName"]),
                                Usr_LName = Convert.ToString(_Reader["Usr_LName"]),
                                Usr_IdentNum = Convert.ToString(_Reader["Usr_IdentNum"]),
                                Usr_Gender = Convert.ToInt16(_Reader["Usr_Gender"]),
                                Usr_Mail = Convert.ToString(_Reader["Usr_Mail"]),
                                Usr_Mobile = Convert.ToString(_Reader["Usr_Mobile"]),
                                Usr_DateReg = Convert.ToString(_Reader["Usr_DateReg"]),
                                Usr_UName = Convert.ToString(_Reader["Usr_UName"]),
                                Usr_ProvID = Convert.ToInt32(_Reader["Usr_ProvID"]),
                                Usr_CtyID = Convert.ToInt32(_Reader["Usr_CtyID"]),
                                Usr_ProvName = Convert.ToString(_Reader["Usr_ProvName"]),
                                Usr_CtyName = Convert.ToString(_Reader["Usr_CtyName"]),
                                Usr_Address = Convert.ToString(_Reader["Usr_Address"]),
                                Usr_IsA = Convert.ToBoolean(_Reader["Usr_IsA"]),
                                Usr_IsBlk = Convert.ToBoolean(_Reader["Usr_IsBlk"]),
                                Usr_Roles = Convert.ToString(_Reader["Usr_Roles"]),
                                Usr_RolesID = Convert.ToString(_Reader["Usr_RolesID"]),
                                Total_Count = Convert.ToString(_Reader["Total_Count"]),
                            }); ;
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _UserList;
        }

        public async Task<DtoUser_> GetUser_By_ID_SP(int UserID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetUserByID.ToString();
            _Cmd.Parameters.Add(new SqlParameter("User_ID", UserID));
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var _User = new DtoUser_();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _User.Usr_ID = Convert.ToInt32(_Reader["Usr_ID"]);
                        _User.Usr_TnatID = Convert.ToInt32(_Reader["Usr_TnatID"]);
                        _User.Usr_FName = Convert.ToString(_Reader["Usr_FName"]);
                        _User.Usr_LName = Convert.ToString(_Reader["Usr_LName"]);
                        _User.Usr_IdentNum = Convert.ToString(_Reader["Usr_IdentNum"]);
                        _User.Usr_Gender = Convert.ToInt16(_Reader["Usr_Gender"]);
                        _User.Usr_Mail = Convert.ToString(_Reader["Usr_Mail"]);
                        _User.Usr_Mobile = Convert.ToString(_Reader["Usr_Mobile"]);
                        _User.Usr_DateReg = Convert.ToString(_Reader["Usr_DateReg"]);
                        _User.Usr_UName = Convert.ToString(_Reader["Usr_UName"]);
                        _User.Usr_ProvID = Convert.ToInt32(_Reader["Usr_ProvID"]);
                        _User.Usr_CtyID = Convert.ToInt32(_Reader["Usr_CtyID"]);
                        _User.Usr_ProvName = Convert.ToString(_Reader["Usr_ProvName"]);
                        _User.Usr_CtyName = Convert.ToString(_Reader["Usr_CtyName"]);
                        _User.Usr_Address = Convert.ToString(_Reader["Usr_Address"]);
                        _User.Usr_IsA = Convert.ToBoolean(_Reader["Usr_IsA"]);
                        _User.Usr_IsBlk = Convert.ToBoolean(_Reader["Usr_IsBlk"]);
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _User;
        }

        public async Task<bool> IsActiveUser(int User_ID, bool State)
        {
            if (User_ID <= 0) return false;
            var _User = await _ContextUC.User.FirstOrDefaultAsync(U => U.User_ID == User_ID).ConfigureAwait(false);
            if (_User == null) return false;
            _User.User_IsActive = State;
            _ContextUC.Entry(_User).State = EntityState.Modified;
            return true;
        }

        public async Task<bool> IsBlockUser(int User_ID, bool State)
        {
            if (User_ID <= 0) return false;
            var _User = await _ContextUC.User.FirstOrDefaultAsync(U => U.User_ID == User_ID).ConfigureAwait(false);
            if (_User == null) return false;
            _User.User_IsBlock = State;
            _ContextUC.Entry(_User).State = EntityState.Modified;
            return true;
        }

        public async Task<bool> UpdatePassword(int User_ID, string NewPassword)
        {
            if (User_ID <= 0) return false;
            var _User = await _ContextUC.User.FirstOrDefaultAsync(U => U.User_ID == User_ID).ConfigureAwait(false);
            if (_User == null) return false;
            _User.User_HashPassword = NewPassword;
            _User.User_IsActive = true;
            _ContextUC.Entry(_User).State = EntityState.Modified;
            return true;
        }
    }
}


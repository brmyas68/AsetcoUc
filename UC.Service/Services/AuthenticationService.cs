using System.Text;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using UC.Common.Mapping;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Data.SqlClient;
using static UC.Common.StoredProcedure.SPUC;
using UC.Common.StoredProcedure;

namespace UC.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ContextUC _ContextUC;
        public AuthenticationService(ContextUC ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<int> CountUser(int User_ID)
        {
            if (User_ID <= 0) return 0;
            var _Token_Count = await _ContextUC.Token.Where(T => T.Token_UserID == User_ID).CountAsync().ConfigureAwait(false);
            return _Token_Count;
        }

        public string CreateHashing(string Value)
        {
#pragma warning disable
            var hashAlgorithm = new SHA512CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(Value);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        public async Task<string> CreateToken(int User_ID, DtoSettingToken DtoSettingToken)
        {
            var _Claims = new List<Claim>();
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Sub, DtoSettingToken.Subject));
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, DtoSettingToken.Guid.ToString()));
            _Claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DtoSettingToken.DateTime_UtcNow.ToString()));
            _Claims.Add(new Claim("UserID", User_ID.ToString()));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DtoSettingToken.Signing_Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var _Token = new JwtSecurityToken(
                                    DtoSettingToken.Issuer,
                                    DtoSettingToken.Audience,
                                    _Claims,
                                    expires: DtoSettingToken.DateTime_Now.AddMinutes(DtoSettingToken.Expir_Minutes),
                                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        public async Task<bool> IsValidAuthoriz(int User_ID, string Role_Name)
        {
            if (User_ID <= 0) return false;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPName.UC_GetRoleNamesByUserID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("User_ID", User_ID));
            var _UserRoleNameList = new List<string>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _UserRoleNameList.Add(_Reader["Role_Names"].ToString());
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _UserRoleNameList.Contains(Role_Name);
        }

        public async Task<bool> IsValidToken(int User_ID, string Token_Hash)
        {
            bool _isValid = false;
            if (User_ID <= 0 && Token_Hash == "") return false;
            var _Token = (await _ContextUC.Token.FromSqlRaw(SPUC.SP_FindToken,
                                                                 new SqlParameter("Token_UserID", User_ID),
                                                                 new SqlParameter("Token_HashCode", Token_Hash))
                                                                 .ToListAsync().ConfigureAwait(false))
                                                                 .FirstOrDefault();
            if (_Token != null)
            {
                if (DateTime.Now.ToString().ToLower().CompareTo(_Token.Token_DateExpire.ToString().ToLower()) <= 0)
                    _isValid = true;
                else
                {
                    DateTime _DateTimeLast = _Token.Token_DateLastAccessTime;
                    TimeSpan _Diff_Date = DateTime.Now.Subtract(_DateTimeLast);
                    var _Minutes = _Diff_Date.Minutes;
                    if (_Minutes <= 30)
                        _isValid = true;
                }
                if (_isValid)
                {
                    _Token.Token_DateExpire = DateTime.Now;
                    _ContextUC.Entry(_Token).State = EntityState.Modified;
                    int _Res = await _ContextUC.SaveChangesAsync();
                    return _isValid;
                }
                else
                {
                    _ContextUC.Entry(_Token).State = EntityState.Deleted;
                    int _Res = await _ContextUC.SaveChangesAsync();
                    return _isValid;
                }
            }
            return _isValid;
        }

        public string DeCryptASE(string key, string value)
        {
            byte[] iv = new byte[16]; byte[] buffer = Convert.FromBase64String(value);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return "";
        }

        public string EnCryptASE(string key, string text)
        {
            byte[] iv = new byte[16]; byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key); aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream)) streamWriter.Write(text);
                        array = memoryStream.ToArray();
                        return Convert.ToBase64String(array);
                    }
                }
            }
            return "";
        }

        public async Task<int> CheckPermissions(int User_ID, int Tenant_ID, string TagName_Form, string TagName_Action)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_CheckPermissions.ToString();
            _Cmd.Parameters.Add(new SqlParameter("UserID", User_ID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", Tenant_ID));
            _Cmd.Parameters.Add(new SqlParameter("TagNameForm", TagName_Form));
            _Cmd.Parameters.Add(new SqlParameter("TagNameAction", TagName_Action));
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var _CountFound = 0;
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _CountFound = Convert.ToInt32(_Reader["Count_Found"]);
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _CountFound;
        }
    }
}

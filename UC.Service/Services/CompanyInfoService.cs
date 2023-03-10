

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;


namespace UC.Service.Services
{
    public class CompanyInfoService : BaseService<CompanyInfo>, ICompanyInfoService
    {
        private readonly ContextUC _ContextUC;
        public CompanyInfoService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<int> DefaultLanguageCompany(int TenantID)
        {
            var _LanguageCompany = await _ContextUC.CompanyInfo.Where(C => C.CompanyInfo_ID == TenantID).FirstOrDefaultAsync().ConfigureAwait(false);
            if (_LanguageCompany == null) return -1;
            return _LanguageCompany.CompanyInfo_LanguageID;
        }

        public async Task<List<DtoCompanyInfo_>> GetAll_SP(int LangID, int AnnexSettingID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllCompany.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter Parameter = new SqlParameter();
            Parameter.ParameterName = "LangID";
            Parameter.SqlDbType = SqlDbType.Int;
            Parameter.Value = LangID;
            _Cmd.Parameters.Add(Parameter);

            SqlParameter ParameterAnnexSetting = new SqlParameter();
            ParameterAnnexSetting.ParameterName = "AnexSetingID";
            ParameterAnnexSetting.SqlDbType = SqlDbType.Int;
            ParameterAnnexSetting.Value = AnnexSettingID;
            _Cmd.Parameters.Add(ParameterAnnexSetting);


            var _DtoCompanyInfo_ = new List<DtoCompanyInfo_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoCompanyInfo_.Add(
                            new DtoCompanyInfo_
                            {
                                TagType = Convert.ToInt32(_Reader["TagType"]),
                                TagName = _Reader["TagName"].ToString(),
                                TransTag = _Reader["TransTag"].ToString(),
                                CoIn_ID = Convert.ToInt32(_Reader["CompanyInfo_ID"]),
                                CoIn_LangID = Convert.ToInt32(_Reader["CompanyInfo_LanguageID"]),
                                CoIn_TagID = Convert.ToInt32(_Reader["CompanyInfo_TagID"]),
                                CoIn_TypeDateTime = Convert.ToInt32(_Reader["CompanyInfo_TypeDateTime"]),
                                CoIn_Address = _Reader["CompanyInfo_Address"].ToString(),
                                CoIn_About = _Reader["CompanyInfo_About"].ToString(),
                                CoIn_Email = _Reader["CompanyInfo_Email"].ToString(),
                                CoIn_Phone = _Reader["CompanyInfo_Phone"].ToString(),
                                CoIn_Mobile = _Reader["CompanyInfo_Mobile"].ToString(),
                                CoIn_Fax = _Reader["CompanyInfo_Fax"].ToString(),
                                CoIn_Site = _Reader["CompanyInfo_Site"].ToString(),
                                CoIn_Instagram = _Reader["CompanyInfo_Instagram"].ToString(),
                                CoIn_SmsNumber = _Reader["CompanyInfo_SmsNumber"].ToString(),
                                CoIn_LangName = _Reader["LangName"].ToString(),
                                CoIn_Logo = _Reader["Path_Image"].ToString(),
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
            return _DtoCompanyInfo_;
        }
    }
}



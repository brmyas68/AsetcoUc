



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
    public class TranslateTagsService : BaseService<TranslateTags>, ITranslateTagsService
    {
        private readonly ContextUC _ContextUC;
        public TranslateTagsService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<DtoTranslateTags_>> GetAll_By_Language_ID(int LanguageID)
        {
            if (LanguageID <= 0)
                return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetTranslateTagsByLangID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TranslateTags_LanguageID", LanguageID));
            var _DtoTranslateTags = new List<DtoTranslateTags_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTranslateTags.Add(
                            new DtoTranslateTags_
                            {
                                TranTg_ID = Convert.ToInt32(_Reader["TransID"]),
                                TranTg_LangID = Convert.ToInt32(_Reader["TransLangID"]),
                                TranTg_LangName = Convert.ToString(_Reader["LangName"]),
                                TranTg_TagID = Convert.ToInt32(_Reader["TransTagID"]),
                                TranTg_Text = Convert.ToString(_Reader["TransTagText"]),
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
            return _DtoTranslateTags;
        }

        public async Task<DtoTranslateTags_> GetAll_By_Language_Tagsknowledge_ID(int LanguageID, int TagsknowledgeID)
        {
            if (LanguageID <= 0 && TagsknowledgeID <= 0)
                return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetTranslateTagsByLangTagID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TranslateTags_LanguageID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("TranslateTags_TagsknowledgeID", TagsknowledgeID));
            var _DtoTranslateTags = new List<DtoTranslateTags_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTranslateTags.Add(
                            new DtoTranslateTags_
                            {
                                TranTg_ID = Convert.ToInt32(_Reader["TransID"]),
                                TranTg_LangID = Convert.ToInt32(_Reader["TransLangID"]),
                                TranTg_LangName = Convert.ToString(_Reader["LangName"]),
                                TranTg_TagID = Convert.ToInt32(_Reader["TransTagID"]),
                                TranTg_Text = Convert.ToString(_Reader["TransTagText"]),
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
            return _DtoTranslateTags.FirstOrDefault();
        }

        public async Task<IList<DtoTranslateTags_>> GetAll_By_Tagsknowledge_ID(int TagsknowledgeID)
        {
            if (TagsknowledgeID <= 0)
                return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetTranslateTagsByTagID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TranslateTags_TagsknowledgeID", TagsknowledgeID));
            var _DtoTranslateTags = new List<DtoTranslateTags_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTranslateTags.Add(
                            new DtoTranslateTags_
                            {
                                TranTg_ID = Convert.ToInt32(_Reader["TransID"]),
                                TranTg_LangID = Convert.ToInt32(_Reader["TransLangID"]),
                                TranTg_LangName = Convert.ToString(_Reader["LangName"]),
                                TranTg_TagID = Convert.ToInt32(_Reader["TransTagID"]),
                                TranTg_Text = Convert.ToString(_Reader["TransTagText"]),
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
            return _DtoTranslateTags;
        }

        public async Task<List<DtoTranslateTagsLogin>> GetAll_By_Tranlate_Tags_Login(int LanguageID, string Tags_Names)
        {
            if (LanguageID <= 0 && Tags_Names == "")
                return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_LoginTranslateTags.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("Language_ID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("Tags_Name", Tags_Names));
            var _DtoTranslateTagsLogin = new List<DtoTranslateTagsLogin>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTranslateTagsLogin.Add(
                            new DtoTranslateTagsLogin
                            {

                                TranTg_TagID = Convert.ToInt32(_Reader["TagID"]),
                                TranTg_TagName = Convert.ToString(_Reader["TagName"]),
                                TranTg_Text = Convert.ToString(_Reader["TransTag"]),

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
            return _DtoTranslateTagsLogin;
        }
    }
}


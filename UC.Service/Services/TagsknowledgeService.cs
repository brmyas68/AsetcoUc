
using UC.ClassDomain.Domains;


using Microsoft.EntityFrameworkCore;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.DataLayer.Contex;
using UC.Interface.Interfaces;
using UC.Service.ServiceBase;
using Microsoft.Data.SqlClient;
using UC.Common.StoredProcedure;

namespace UC.Service.Services
{
    public class TagsknowledgeService : BaseService<Tagsknowledge>, ITagsknowledgeService
    {
        private readonly ContextUC _ContextUC;
        public TagsknowledgeService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<List<Tagsknowledge_>> GetAllRole_SP(int TagType)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRole.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TagType", TagType));
            var _Tagsknowledge = new List<Tagsknowledge_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _Tagsknowledge.Add(
                            new Tagsknowledge_
                            {
                                Tag_ID = Convert.ToInt32(_Reader["TagID"]),
                                Tag_Name = Convert.ToString(_Reader["TagName"]),
                                Tag_TransName = Convert.ToString(_Reader["TransTag"]),
                                Tag_PID = Convert.ToInt32(_Reader["TagPID"]),
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
            return _Tagsknowledge;
        }

        public async Task<IList<DtoTagsknowledge_>> GetAll_Tags_Translate(int Language_ID, int TagType, int ParentID)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllTagsTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TagType", TagType));
            _Cmd.Parameters.Add(new SqlParameter("ParentID", ParentID));
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", Language_ID));
            var _DtoTagsknowledge = new List<DtoTagsknowledge_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTagsknowledge.Add(
                            new DtoTagsknowledge_
                            {
                                Tag_ID = Convert.ToInt32(_Reader["Tag_ID"]),
                                Tag_Type = Convert.ToInt32(_Reader["Tag_Type"]),
                                Tag_Name = Convert.ToString(_Reader["Tag_Name"]),
                                TagTranslate_Name = Convert.ToString(_Reader["TagTranslate_Name"]),
                                Tag_ParentID = Convert.ToInt32(_Reader["Tag_ParentID"]),

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
            return _DtoTagsknowledge;
        }

        public async Task<IList<Tagsknowledge_>> GetAll_List_TagsTranslate(int Language_ID, string TagNameForm)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_TagsTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("TagNameForm", TagNameForm));
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", Language_ID));
            var _DtoTagsknowledge = new List<Tagsknowledge_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoTagsknowledge.Add(
                            new Tagsknowledge_
                            {
                                Tag_ID = Convert.ToInt32(_Reader["TagID"]),
                                Tag_Name = Convert.ToString(_Reader["TagName"]),
                                Tag_TransName = Convert.ToString(_Reader["TransTag"]),

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
            return _DtoTagsknowledge;
        }
    }
}

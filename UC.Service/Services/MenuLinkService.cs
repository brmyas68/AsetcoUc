

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.InterfaceService.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class MenuLinkService : BaseService<MenuLink>, IMenuLinkService
    {
        private readonly ContextUC _ContextUC;
        public MenuLinkService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<List<DtoMenuLink_>> GetAll_SP(int LangID, int SystemTagID)
        {

            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllMenuLinkTranslate.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("SystemTagID", SystemTagID));
            var _DtoMenuLinks = new List<DtoMenuLink_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoMenuLinks.Add(
                            new DtoMenuLink_
                            {
                                MenuLnk_ID = Convert.ToInt32(_Reader["MenuLink_ID"]),
                                MenuLink_TransTagName = _Reader["MenuLink_TransTagName"].ToString(),
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
            return _DtoMenuLinks;
        }

        public async Task<List<DtoMenuLinkGrid_>> GetAll_Grid()
        {

            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_MenuLinkTransTagName.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var _DtoMenuLinkGrid = new List<DtoMenuLinkGrid_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoMenuLinkGrid.Add(
                            new DtoMenuLinkGrid_
                            {
                                Form_TagName = _Reader["Form_TagName"].ToString(),
                                Action_TagName = _Reader["Action_TagName"].ToString(),
                                Link_TagName = _Reader["Link_TagName"].ToString(),
                                System_TagName = _Reader["System_TagName"].ToString(),
                                MenuLink_ID = Convert.ToInt32(_Reader["MenuLink_ID"]),
                                Parent_TagName = _Reader["Parent_TagName"].ToString(),
                                MenuLink_TypeRouteID = Convert.ToInt32(_Reader["MenuLink_TypeRouteID"]),
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
            return _DtoMenuLinkGrid;
        }

        public async Task<List<DtoMenuLink_>> GetAllSubMenu_SP(int LangID, int SystemTagID)
        {

            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllSubMenuLink.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("SystemTagID", SystemTagID));
            var _DtoMenuLinks = new List<DtoMenuLink_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoMenuLinks.Add(
                            new DtoMenuLink_
                            {
                                MenuLnk_ID = Convert.ToInt32(_Reader["Link_ID"]),
                                MenuLink_TransTagName = _Reader["Link_TagTrans"].ToString(),
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
            return _DtoMenuLinks;
        }

    }
}

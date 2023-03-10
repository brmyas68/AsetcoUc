

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
    public class RouteStructureService : BaseService<RouteStructure>, IRouteStructureService
    {
        private readonly ContextUC _ContextUC;
        public RouteStructureService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_Parent_ID(int LanguageID, int ParentID)
        {
            if (LanguageID <= 0 && ParentID < 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRouteStructureByParentID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("RouteStructure_ParentID", ParentID));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_Parent_TypeRoute_ID(int LanguageID, int ParentID, int TypeRoute)
        {
            if (LanguageID <= 0 && TypeRoute < 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRouteStructureByParentTypeRoute.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("RouteStructure_ParentID", ParentID));
            _Cmd.Parameters.Add(new SqlParameter("RouteStructure_TypeRoute", TypeRoute));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute(int LanguageID)
        {
            if (LanguageID <= 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRouteStructureByTypeRouteAll.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute(int LanguageID, int TypeRoute)
        {
            if (LanguageID <= 0 && TypeRoute < 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SP_GetRouteStructureByTypeRoute.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("RouteStructure_TypeRoute", TypeRoute));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRouteAll(int LanguageID)
        {
            if (LanguageID <= 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRouteStructureByTypeRouteAll.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_By_TypeRoute_For_Tree(int LanguageID)
        {
            if (LanguageID <= 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRouteStructureForTree.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_Form_RouteStructure(int LangID, int TenantID, string TagNameForm)
        {
            if (LangID <= 0 && TagNameForm == "") return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllFormRouteStructure.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("TenantID", TenantID));
            _Cmd.Parameters.Add(new SqlParameter("TagNameForm", TagNameForm));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                            new Dto_RouteStructure_
                            {
                                RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]),
                                RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]),
                                RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]),
                                RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString(),
                                RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString(),
                                RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]),
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
            return _RouteStructureList;
        }

        public async Task<IList<Dto_RouteStructure_>> GetAll_Route_RouteStructureTreeByTagID(int LangID, int TagID,int Tenant_ID , int TypeRout)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllRouteStructurTreeByTagID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("Lang_ID", LangID));
            _Cmd.Parameters.Add(new SqlParameter("Tag_ID", TagID));
            _Cmd.Parameters.Add(new SqlParameter("Tenant_ID", Tenant_ID));
            _Cmd.Parameters.Add(new SqlParameter("TypeRout", TypeRout));
            var _RouteStructureList = new List<Dto_RouteStructure_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _RouteStructureList.Add(
                          new Dto_RouteStructure_
                          {
                              RoutStr_ID = Convert.ToInt32(_Reader["R_ID"]),
                              RoutStr_PID = Convert.ToInt32(_Reader["R_ParentID"]),
                              RoutStr_Tag_ID = Convert.ToInt32(_Reader["R_TagID"]),
                              RoutStr_Tag_Name = _Reader["Tn_TagName"].ToString(),
                              RoutStr_Trans_Tag_Name = _Reader["R_FormName"].ToString(),
                              RoutStr_TypeRout = Convert.ToInt16(_Reader["R_TypeRoute"]),
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
            return _RouteStructureList;
        }

        public async Task<Dto_RouteStructure_> GetRouteStructure_By_ID_SP(int LanguageID, int RouteStructureID)
        {
            if (RouteStructureID <= 0) return null;
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen) await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetRouteStructureByRouteID.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("LanguageID", LanguageID));
            _Cmd.Parameters.Add(new SqlParameter("RouteStructure_ID", RouteStructureID));
            var _RouteStructureList = new Dto_RouteStructure_();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        {
                            _RouteStructureList.RoutStr_ID = Convert.ToInt32(_Reader["RoutStr_ID"]);
                            _RouteStructureList.RoutStr_PID = Convert.ToInt32(_Reader["RoutStr_PID"]);
                            _RouteStructureList.RoutStr_Tag_ID = Convert.ToInt32(_Reader["RoutStr_Tag_ID"]);
                            _RouteStructureList.RoutStr_Tag_Name = _Reader["RoutStr_Tag_Name"].ToString();
                            _RouteStructureList.RoutStr_Trans_Tag_Name = _Reader["RoutStr_Trans_Tag_Name"].ToString();
                            _RouteStructureList.RoutStr_TypeRout = Convert.ToInt16(_Reader["RoutStr_TypeRout"]);
                        };
                    }
                }
                finally
                {
                    await _Reader.CloseAsync().ConfigureAwait(false);
                    if (IsOpen)
                        await _Cmd.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return _RouteStructureList;
        }
    }
}


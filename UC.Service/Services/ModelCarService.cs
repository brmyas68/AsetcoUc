

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.InterfaceService.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class ModelCarService : BaseService<ModelCar>, IModelCarService
    {
        private readonly ContextUC _ContextUC;
        public ModelCarService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<DtoModelCar_>> GetAll_SP()
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllModelCar.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var _DtoModelCar = new List<DtoModelCar_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoModelCar.Add(
                            new DtoModelCar_
                            {
                                ModCar_ID = Convert.ToInt32(_Reader["ModelCar_ID"]),
                                ModCar_BrdCarID = Convert.ToInt32(_Reader["ModelCar_BrandCarID"]),
                                ModCar_Typ = Convert.ToInt32(_Reader["BrandCar_Type"]),
                                ModCar_Name = Convert.ToString(_Reader["ModelCar_Name"]),
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
            return _DtoModelCar;
        }
    }
}

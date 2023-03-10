

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UC.ClassDomain.Domains;
using UC.ClassDTO.DTOs.Custom;
using UC.Common.StoredProcedure;
using UC.DataLayer.Contex;
using UC.InterfaceService.Interfaces;
using UC.Service.ServiceBase;

namespace UC.Service.Services
{
    public class BrandCarService : BaseService<BrandCar>, IBrandCarService
    {
        private readonly ContextUC _ContextUC;
        public BrandCarService(ContextUC ContextUC) : base(ContextUC)
        {
            _ContextUC = ContextUC;
        }

        public async Task<IList<DtoBrandModelCar_>> GetBrandModelCar_SP(int BrandType)
        {
            var _Cmd = _ContextUC.Database.GetDbConnection().CreateCommand();
            bool IsOpen = _Cmd.Connection.State == System.Data.ConnectionState.Open;
            if (!IsOpen)
                await _Cmd.Connection.OpenAsync().ConfigureAwait(false);
            _Cmd.CommandText = SPUC.SPName.UC_GetAllBrandCar.ToString();
            _Cmd.CommandType = System.Data.CommandType.StoredProcedure;
            _Cmd.Parameters.Add(new SqlParameter("BrandType", BrandType));
            var _DtoBrandModelCar = new List<DtoBrandModelCar_>();
            using (var _Reader = await _Cmd.ExecuteReaderAsync().ConfigureAwait(false))
            {
                try
                {
                    while (await _Reader.ReadAsync().ConfigureAwait(false))
                    {
                        _DtoBrandModelCar.Add(
                            new DtoBrandModelCar_
                            {
                                BrdCar_ID = Convert.ToInt32(_Reader["BrandCar_ID"]),
                                ModCar_ID = Convert.ToInt32(_Reader["ModelCar_ID"]),
                                BrdModCar_Name = Convert.ToString(_Reader["BrandModel_Name"]),
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
            return _DtoBrandModelCar;
        }

    }
}



using cts_twister_api.model;
using cts_twister_api.model.employee;
using cts_twister_api.model.production;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.handler
{
    public class ProductionHandler
    {
        public static async Task<MDResponse<List<MDHourByHourReport>>> GetHourByHourReportByMachine(string shift, string station, string plant, string con)
        {
            var res = new MDResponse<List<MDHourByHourReport>>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@shift", shift);
                    prms.Add("@station", station);
                    prms.Add("@plant", plant);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    List<MDHourByHourReport> data = [..(await connection.QueryAsync<MDHourByHourReport>("Sp_TWNV_PRODUCTION_GET_HOUR_BY_HOUR_REPORT_BY_MACHINE", prms, commandType: CommandType.StoredProcedure))];

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    res.Result = result;
                    res.Message = prms.Get<string>("@message");
                    res.Data = data;
                }
            }
            catch (Exception ex)
            {
                res.Result = ResultResponse.error_exception;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return res;
        }

        public static async Task<MDResponse<MDKPI>> GetHourKPIByMachine(string station, string plant, string con)
        {
            var res = new MDResponse<MDKPI>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@station", station);
                    prms.Add("@plant", plant);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDKPI data = await connection.QueryFirstOrDefaultAsync<MDKPI>("Sp_TWNV_PRODUCTION_GET_KPI_BY_MACHINE", prms, commandType: CommandType.StoredProcedure);

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    res.Result = result;
                    res.Message = prms.Get<string>("@message");
                    res.Data = data;
                }
            }
            catch (Exception ex)
            {
                res.Result = ResultResponse.error_exception;
                res.Message = ex.Message;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return res;
        }
    }
}

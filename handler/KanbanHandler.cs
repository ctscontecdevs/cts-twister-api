using cts_twister_api.model;
using cts_twister_api.model.kanban;
using cts_twister_api.model.production;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.handler
{
    public class KanbanHandler
    {
        public static async Task<MDResponse<MDKanbanInfo>> GetKanbanInfo(string serial_tadiff, string con)
        {
            var connection = new SqlConnection(con);
            var res = new MDResponse<MDKanbanInfo>();
            var data = new MDKanbanInfo();
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@serialTadiff", serial_tadiff);
                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    var responseSql = await connection.QueryMultipleAsync("Sp_TWNV_KANBAN_GET_KANBAN_INFO", prms, commandType: CommandType.StoredProcedure);

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    res.Result = result;
                    res.Message = prms.Get<string>("@message");

                    if (res.Result == ResultResponse.success)
                    {
                        data.Kanban = await responseSql.ReadFirstOrDefaultAsync<MDKanban>();
                        data.CircuitDetailList = (await responseSql.ReadAsync<MDCuircuitDetail>()).ToList();
                        data.StripLenght = await responseSql.ReadFirstOrDefaultAsync<MDStripLenght>();
                        res.Data = data;
                    }
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

        public static async Task<MDResponse<MDCircuirtInformation>> GetKanbanCircuitInfo(string serial_tadiff, string con)
        {
            var connection = new SqlConnection(con);
            var res = new MDResponse<MDCircuirtInformation>();
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@serialTadiff", serial_tadiff);
                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDCircuirtInformation data = await connection.QueryFirstOrDefaultAsync<MDCircuirtInformation>("Sp_TWNV_KANBAN_GET_CIRCUIT_INFO", prms, commandType: CommandType.StoredProcedure);

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

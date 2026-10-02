using cts_twister_api.model;
using cts_twister_api.model.esquematico;
using cts_twister_api.model.production;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.handler
{
    public class EsquematicoHandler
    {
        public static async Task<MDResponse<MDEsquematico>> GetEsquematicoInfo(string esquematico, string con)
        {
            var res = new MDResponse<MDEsquematico>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@esquematico", esquematico);
                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDEsquematico data = await connection.QueryFirstOrDefaultAsync<MDEsquematico>("Sp_TWNV_ESQUEMATICO_GET_ESQUEMATICO_INFO", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<MDAjustment>> GetAdjustment(string esquematico_name, string station, string plant, string con)
        {
            var res = new MDResponse<MDAjustment>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@esquematico", esquematico_name);
                    prms.Add("@station", station);
                    prms.Add("@plant", plant);
                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDAjustment data = await connection.QueryFirstOrDefaultAsync<MDAjustment>("Sp_TWNV_ESQUEMATICO_GET_ADJUSTMENT", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<int>> PostAdjustment(MDAjustment data, MDHostInfo hostInfo, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@idMachine", data.IdMachine);
                    prms.Add("@esquematico", data.Esquematico);
                    prms.Add("@longitudCable", data.Longitud);
                    prms.Add("@retorno", data.Retorno);
                    prms.Add("@vueltas", data.Vueltas);
                    prms.Add("@reversa", data.Reversa);

                    prms.Add("@hostIp", hostInfo.HostIp);
                    prms.Add("@hostName", hostInfo.HostName);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_ESQUEMATICO_INSERT_ADJUSTMENT", prms, commandType: CommandType.StoredProcedure);

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    response.Result = result;
                    response.Message = prms.Get<string>("@message");
                    response.Data = scopeId;
                }
            }
            catch (Exception ex)
            {
                response.Result = ResultResponse.error_exception;
                response.Message = ex.Message;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return response;
        }
    }
}

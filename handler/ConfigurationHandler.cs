using cts_twister_api.model;
using cts_twister_api.model.configuration;
using cts_twister_api.model.employee;
using cts_twister_api.model.shift;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.handler
{
    public class ConfigurationHandler
    {

        public static async Task<MDResponse<MDMachineConfigurationDetail>> GetConfiguration(string machine_identifier, string con)
        {
            var res = new MDResponse<MDMachineConfigurationDetail>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@machineIdentifier", machine_identifier);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDMachineConfigurationDetail data = await connection.QueryFirstOrDefaultAsync<MDMachineConfigurationDetail>("Sp_TWNV_CONFIGURATION_GET_CONFIGURATION", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<int>> PostMachineConfiguration(MDMachineConfiguration data, MDHostInfo hostInfo, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@machineIdentifier", data.MachineIdentifier);
                    prms.Add("@idMachine", data.IdMachine);

                    prms.Add("@hostIp", hostInfo.HostIp);
                    prms.Add("@hostName", hostInfo.HostName);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_CONFIGURATION_INSERT_CONFIGURATION", prms, commandType: CommandType.StoredProcedure);

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

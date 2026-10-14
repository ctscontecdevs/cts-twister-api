using cts_twister_api.model;
using cts_twister_api.model.employee;
using cts_twister_api.model.production;
using cts_twister_api.model.shift;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;
using static Dapper.SqlMapper;

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

                    List<MDHourByHourReport> data = [.. (await connection.QueryAsync<MDHourByHourReport>("Sp_TWNV_PRODUCTION_GET_HOUR_BY_HOUR_REPORT_BY_MACHINE", prms, commandType: CommandType.StoredProcedure))];

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


        public static async Task<MDResponse<List<MDProductionReport>>> GetProductionReport(int machine_id, MDHostInfo hostInfo, string con)
        {
            var res = new MDResponse<List<MDProductionReport>>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@machineId", machine_id);
                    prms.Add("@hostName", hostInfo.HostName);
                    prms.Add("@hostIp", hostInfo.HostIp);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    List<MDProductionReport> data = [.. (await connection.QueryAsync<MDProductionReport>("Sp_TWNV_PRODUCTION_CREATE_PRODUCTION_REPORT", prms, commandType: CommandType.StoredProcedure))];

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

        public static async Task<MDResponse<MDPlantProduction>> GetPlantProductionReport(DateTime report_date, string con)
        {
            var connection = new SqlConnection(con);
            var response = new MDResponse<MDPlantProduction>();
            var data = new MDPlantProduction();
            try
            {
                using (connection)
                {
                    Settings.CommandTimeout = 0;
                    var prms = new DynamicParameters();
                    prms.Add("@reportDate", report_date);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    var res = await connection.QueryMultipleAsync("Sp_TWNV_PRODUCTION_GET_REPORT_BY_PLANT", prms, commandType: CommandType.StoredProcedure);

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    if (result == ResultResponse.success)
                    {
                        data.PlantProductionByHours = ((List<MDPlantProductionByHour>)await res.ReadAsync<MDPlantProductionByHour>());
                        data.PlantProductionByMachineTypes = ((List<MDPlantProductionByMachineType>)await res.ReadAsync<MDPlantProductionByMachineType>());
                        data.PlantProductionByMachine = ((List<MDPlantProductionByMachine>)await res.ReadAsync<MDPlantProductionByMachine>());

                        response.Data = data;
                    }
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


        public static async Task<MDResponse<int>> PostProduction(MDProduction data, MDHostInfo hostInfo, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@caja", data.Box);
                    prms.Add("@shift", data.Shift);
                    prms.Add("@station", data.Station);
                    prms.Add("@plant", data.Plant);
                    prms.Add("@employeeNo", data.EmployeeNo);

                    prms.Add("@hostIp", hostInfo.HostIp);
                    prms.Add("@hostName", hostInfo.HostName);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_PRODUCTION_INSERT_PRODUCTION", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<int>> ProductionDetail(MDProductionDetail data, MDHostInfo hostInfo, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@idMachine", data.IdMachine);
                    prms.Add("@shift", data.Shift);
                    prms.Add("@employeeNo", data.EmployeeNo);
                    prms.Add("@idBox", data.IdBox);
                    prms.Add("@esquematicoName", data.EsquematicoName);
                    prms.Add("@pieces", data.Pieces);
                    prms.Add("@length", data.Length);
                    prms.Add("@length2", data.Length2);
                    prms.Add("@pitch", data.Pitch);
                    prms.Add("@lengthInit", data.Length_ini);
                    prms.Add("@lengthInitNotTw", data.Length_ini_not_twister);

                    prms.Add("@hostIp", hostInfo.HostIp);
                    prms.Add("@hostName", hostInfo.HostName);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_PRODUCTION_INSERT_PRODUCTION_DETAIL", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<int>> PostTwisterVerified(MDTwisterVerified data, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@idMachine", data.IdMachine);
                    prms.Add("@employeeNo", data.EmployeeNo);
                    prms.Add("@serial", data.Serial);
                    prms.Add("@idProceso", data.IdProcess);
                    prms.Add("@esquematicoName", data.EsquemticoName);
                    prms.Add("@employeeNo", data.EmployeeNo);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_PRODUCTION_INSERT_TWISTER_VERIFIED", prms, commandType: CommandType.StoredProcedure);

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

        public static async Task<MDResponse<int>> PostProductionDetailTadiff(MDProductionSerial data, string con)
        {
            var response = new MDResponse<int>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@idProduction", data.IdProduction);
                    prms.Add("@serial", data.Serial);
                    prms.Add("@lotCode", data.LotCode);
                    prms.Add("@generalCode", data.General);
                    prms.Add("@circuit", data.CircuitId);
                    prms.Add("@wireSize", data.WireSize);
                    prms.Add("@wireColor", data.WireColor);
                    prms.Add("@wireType", data.WireType);
                    prms.Add("@wireCutLenght", data.WireCutLength);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    int scopeId = await connection.QueryFirstOrDefaultAsync<int>("Sp_TWNV_PRODUCTION_INSERT_PRODUCTION_DETAIL_SERIAL", prms, commandType: CommandType.StoredProcedure);

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



        public static async Task<MDResponse<bool>> UpdateComment(MDComment data, string con)
        {
            var response = new MDResponse<bool>();
            var connection = new SqlConnection(con);
            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();

                    prms.Add("@id", data.Id);
                    prms.Add("@comment", data.Comment);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    bool res = await connection.QueryFirstOrDefaultAsync<bool>("Sp_TWNV_PRODUCTION_UPD_PRODUCTION_REPORT_COMMENT", prms, commandType: CommandType.StoredProcedure);

                    _ = Enum.TryParse(prms.Get<string>("@result"), out ResultResponse result);

                    response.Result = result;
                    response.Message = prms.Get<string>("@message");
                    response.Data = res;
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

using cts_twister_api.model;
using cts_twister_api.model.employee;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static cts_twister_api.common.ResEnumerators;

namespace cts_twister_api.handler
{
    public class EmployeeHandler
    {
        public static async Task<MDResponse<MDEmployee>> GetEmployee(string employee_no,  string con)
        {
            var res = new MDResponse<MDEmployee>();
            var connection = new SqlConnection(con);

            try
            {
                using (connection)
                {
                    var prms = new DynamicParameters();
                    prms.Add("@employeeNo", employee_no);

                    prms.Add("@result", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                    prms.Add("@message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                    MDEmployee data = await connection.QueryFirstOrDefaultAsync<MDEmployee>("Sp_TWNV_EMPLOYEE_GET_EMPLOYEE", prms, commandType: CommandType.StoredProcedure);

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

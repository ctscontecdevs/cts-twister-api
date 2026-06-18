using cts_twister_api.baseproject;
using cts_twister_api.common;
using cts_twister_api.handler;
using cts_twister_api.model.shift;

namespace cts_twister_api.defs
{
    public class TwisterDef : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var conDB_TwisterSystem = app.Configuration.GetConnectionString("conDB_TwisterSystem");

            #region MapGroup Sesion
            var mgSesion = app.MapGroup("/api/sesion").WithTags("Sesion");

            mgSesion.MapGet("/GetValidateSesion", async (string id_plant, string id_machine) =>
            {
                return await TwisterHandler.GetValidateSesion(id_plant, id_machine, conDB_TwisterSystem);
            });
            #endregion

            #region MapGroup Employee
            var mgEmployee = app.MapGroup("/api/employee").WithTags("Employee");

            mgEmployee.MapGet("/GetEmployee", async (string employee_no) =>
            {
                return await EmployeeHandler.GetEmployee(employee_no, conDB_TwisterSystem);
            });
            #endregion

            #region MapGroup Shift
            var mgShift = app.MapGroup("/api/shift").WithTags("Shift");

            mgShift.MapGet("/GetValidateShift", async (int shift) =>
            {
                return await ShiftHandler.GetValidateShift(shift, conDB_TwisterSystem);
            });

            mgShift.MapPost("/", async (MDStartShift data, HttpContext httpContext) =>
            {
                var hostInfo = ResHost.GetHostInfo(httpContext);

                return await ShiftHandler.PostStartShift(data, hostInfo, conDB_TwisterSystem);
            });


            #endregion

        }
    }
}

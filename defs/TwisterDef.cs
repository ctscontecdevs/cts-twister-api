using cts_twister_api.baseproject;
using cts_twister_api.common;
using cts_twister_api.handler;
using cts_twister_api.model.production;
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
                return await SesionHandler.GetValidateSesion(id_plant, id_machine, conDB_TwisterSystem);
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

            #region MapGroup Production
            var mgProduction = app.MapGroup("/api/production").WithTags("Production");

            mgProduction.MapGet("/GetHourByHourReportByMachine", async (string shift, string station, string plant) =>
            {
                return await ProductionHandler.GetHourByHourReportByMachine(shift, station, plant, conDB_TwisterSystem);
            });

            mgProduction.MapGet("/GetHourKPIByMachine", async (string station, string plant) =>
            {
                return await ProductionHandler.GetHourKPIByMachine(station, plant, conDB_TwisterSystem);
            });

            mgProduction.MapPost("/", async (MDProduction data, HttpContext httpContext) =>
            {
                var hostInfo = ResHost.GetHostInfo(httpContext);

                return await ProductionHandler.PostProduction(data, hostInfo, conDB_TwisterSystem);
            });

            #endregion

            #region MapGroup Kanban
            var mgKanban = app.MapGroup("/api/kanban").WithTags("Kanban");

            mgKanban.MapGet("/GetKanbanInfo", async (string serial_tadiff) =>
            {
                return await KanbanHandler.GetKanbanInfo(serial_tadiff, conDB_TwisterSystem);
            });

            #endregion

            #region MapGroup Esquematico
            var mgEsquematico = app.MapGroup("/api/esquematico").WithTags("Esquematico");

            mgEsquematico.MapGet("/GetEsquematicoInfo", async (string esquematico_name) =>
            {
                return await EsquematicoHandler.GetEsquematicoInfo(esquematico_name, conDB_TwisterSystem);
            });

            mgEsquematico.MapGet("/GetAdjustment", async (string esquematico_name, string station, string plant) =>
            {
                return await EsquematicoHandler.GetAdjustment(esquematico_name, station, plant, conDB_TwisterSystem);
            });

            #endregion

        }
    }
}

using cts_twister_api.baseproject;
using cts_twister_api.handler;

namespace cts_twister_api.defs
{
    public class TwisterDef : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var conDB_TwisterSystem = app.Configuration.GetConnectionString("conDB_TwisterSystem");

            var mgSesion = app.MapGroup("/api/sesion").WithTags("Sesion");

            mgSesion.MapGet("/GetValidateSesion", async (string id_plant, string id_machine) =>
            {
                return await TwisterHandler.GetValidateSesion(id_plant, id_machine, conDB_TwisterSystem);
            });

        }
    }
}

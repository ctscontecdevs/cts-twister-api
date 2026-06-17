namespace cts_twister_api.baseproject
{
    public static class MinimalApiExtensions
    {
        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract
                && !t.IsInterface)
                .Select(Activator.CreateInstance).Cast<IEndpointDefinition>();

            foreach (var endpointdef in endpointDefinitions)
            {
                endpointdef.RegisterEndpoints(app);
            }
        }
    }
}

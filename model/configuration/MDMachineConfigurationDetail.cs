namespace cts_twister_api.model.configuration
{
    public class MDMachineConfigurationDetail: MDMachineConfiguration
    {
        public int Id { get; set; }
        public string MachineCode { get; set; }
        public string MachineDesc { get; set; }
    }
}

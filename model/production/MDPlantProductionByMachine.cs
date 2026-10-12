namespace cts_twister_api.model.production
{
    public class MDPlantProductionByMachine: MDPlantProductionDetail
    {
        public int Id { get; set; }
        public int IdMachine { get; set; }
        public string IdMachineType { get; set; }
        public string MachineName { get; set; }
        public DateTime ReportDate { get; set; }
        public string ShiftId { get; set; }
    }
}

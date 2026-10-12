namespace cts_twister_api.model.production
{
    public class MDPlantProductionByMachineType: MDPlantProductionDetail
    {
        public string ShiftId { get; set; }
        public string HourNo { get; set; }
        public string MachineType { get; set; }
        public string HourSequence { get; set; }
    }
}

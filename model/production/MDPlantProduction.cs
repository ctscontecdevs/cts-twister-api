namespace cts_twister_api.model.production
{
    public class MDPlantProduction
    {
        public List<MDPlantProductionByHour>  PlantProductionByHours { get; set; }
        public List<MDPlantProductionByMachineType> PlantProductionByMachineTypes { get; set; }
        public List<MDPlantProductionByMachine> PlantProductionByMachine { get; set; }
    }
}

namespace cts_twister_api.model.lotcode
{
    public class MDAdvanceByLotCodeDetail
    {
        public string LotCode { get; set; }
        public string Kanban { get; set; }
        public string CircuitCode { get; set; }
        public string LineCode { get; set; }
        public string CircuitId { get; set; }
        public string GroupCode { get; set; }
        public string GeneralCode { get; set; }
        public string ProductNo { get; set; }
        public string ProductNo2 { get; set; }
        public string PlantCode { get; set; }
        public string Process { get; set; }
        public string ProcessPath { get; set; }
        public string MidStation { get; set; }
        public string SideA { get; set; }
        public string SideB { get; set; }
        public string WireType { get; set; }
        public string WireSize { get; set; }
        public string WireColor { get; set; }
        public string WireCutLength { get; set; }
        public string MachineCode { get; set; }
        public string PlanTotal { get; set; }
        public string CutTotal { get; set; }
        public string PlanCutDif { get; set; }
        public string KitsTotal { get; set; }
        public string PlanKitsDifs { get; set; }
        public string ProductionTotal { get; set; }
        public string PlanProductionTotal { get; set; }
        public string PackTotal { get; set; }
        public string PlanPackDif { get; set; }
        public string CircuitStandart { get; set; }
        public DateTime CutDate { get; set; }
        public DateTime AssyDate { get; set; }
    }
}

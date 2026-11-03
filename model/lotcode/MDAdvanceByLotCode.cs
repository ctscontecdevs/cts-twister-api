namespace cts_twister_api.model.lotcode
{
    public class MDAdvanceByLotCode
    {
        public string LotCodeDesc { get; set; }
        public string LotCode { get; set; }
        public int PlanTotal { get; set; }
        public int CorteTotal { get; set; }
        public int KitsTotal { get; set; }
        public int ProductionTotal { get; set; }
        public int PackTotal { get; set; }
        public DateTime CutDate { get; set; }
        public DateTime AssyDate { get; set; }
        public string Color { get; set; }
    }
}

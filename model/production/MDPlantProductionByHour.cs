namespace cts_twister_api.model.production
{
    public class MDPlantProductionByHour: MDPlantProductionDetail
    {
        public string ShiftId { get; set; }
        public string HourSequence { get; set; }
        public string HourNo { get; set; }
        public int PiecesPacked { get; set; }

    }
}

namespace cts_twister_api.model.production
{
    public class MDPlantProductionDetail
    {
        public string HourDesc { get; set; }
        public int PiecesPlanHour { get; set; }
        public int PiecesPlanHourAcc { get; set; }
        public int PiecesRealHour { get; set; }
        public int PiecesRealHourAcc { get; set; }
        public int PiecesDifHour { get; set; }
        public int PiecesDifHourAcc { get; set; }
        public float AdvanceHour { get; set; }
        public float AdvanceHourAcc { get; set; }
        public string ColorRowDif { get; set; }
        public string ColorRowDifAcc { get; set; }
    }
}

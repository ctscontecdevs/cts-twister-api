namespace cts_twister_api.model.production
{
    public class MDPlantProductionDetail
    {
        public string HourDesc { get; set; }
        public string PiecesPlanHour { get; set; }
        public string PiecesPlanHourAcc { get; set; }
        public string PiecesRealHour { get; set; }
        public string PiecesRealHourAcc { get; set; }
        public string PiecesDifHour { get; set; }
        public string PiecesDifHourAcc { get; set; }
        public string AdvanceHour { get; set; }
        public string AdvanceHourAcc { get; set; }
        public string ColorRowDif { get; set; }
        public string ColorRowDifAcc { get; set; }
    }
}

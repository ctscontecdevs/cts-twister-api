namespace cts_twister_api.model.production
{
    public class MDProductionReportByMachineDetail
    {
        public int Id { get; set; }
        public int IdMachine { get; set; }
        public string IdMachineType { get; set; }
        public string MachineName { get; set; }
        public DateTime ReportDate { get; set; }
        public string ShiftId { get; set; }

        public int HourSequence { get; set; }
        public int HourNo { get; set; }
        public int SecondsSchedule { get; set; }

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

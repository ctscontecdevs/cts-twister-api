namespace cts_twister_api.model.production
{
    public class MDProductionReport
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public DateTime ReportDate { get; set; }
        public int ShiftId { get; set; }
        public int SeqHour { get; set; }
        public int NoHour { get; set; }
        public string HourDesc { get; set; }
        public int PlanHour { get; set; }
        public int PlanHourAcc { get; set; }
        public int PiecesHour { get; set; }
        public int PiecesHourAcc { get; set; }
        public int PiecesDif { get; set; }
        public int PiecesDifAcc { get; set; }
        public string Comment { get; set; }
    }
}

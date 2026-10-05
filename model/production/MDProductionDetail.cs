namespace cts_twister_api.model.production
{
    public class MDProductionDetail
    {
        public int IdMachine { get; set; }
        public string Shift { get; set; }
        public string EmployeeNo { get; set; }
        public string IdBox { get; set; }

        public string EsquematicoName { get; set; }
        public int Pieces { get; set; }
        public int Length { get; set; }
        public int Pitch { get; set; }
        public int Length_ini { get; set; }
        public int Length_ini_not_twister { get; set; }
    }
}

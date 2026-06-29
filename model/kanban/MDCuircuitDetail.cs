namespace cts_twister_api.model.kanban
{
    public class MDCuircuitDetail : MDStripLenght
    {
        public string CircuitId { get; set; }
        public string WireSize { get; set; }
        public string WireColor { get; set; }
        public string WireType { get; set; }
        public float WireCutLength { get; set; }
    }
}

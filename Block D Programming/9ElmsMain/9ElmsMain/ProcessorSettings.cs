namespace _9ElmsMain
{
    public class ProcessorSettings
    {
        public short processorId { get; set; }
        public bool isMaster { get; set; }
        public short roomCount { get; set; }
        public short TPCount { get; set; }
        public string[] sonosNames { get; set; }

        public string SIMPLControllerIP { get; set; }
        public int SIMPLControllerPort { get; set; }

        public string masterProcessorIP { get; set; }
        public int masterProcessorPort { get; set; }

        public uint skyTransmitterIPID { get; set; }
    }
}

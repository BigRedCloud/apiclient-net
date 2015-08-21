namespace BigRedCloud.Api.Model
{
    public class BankAccountDto : BaseApiDto
    {
        public long id { get; set; }
        public string acCode { get; set; }
        public string details { get; set; }
        public string lastChq { get; set; }
        public bool isDefaultBank { get; set; }
        public decimal oBalance { get; set; }
        public string nominalAcCode { get; set; }
        public byte[] timestamp { get; set; }
    }
}

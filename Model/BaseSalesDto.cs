namespace BigRedCloud.Api.Model
{
    public abstract class BaseSalesDto : BaseBookTranDto
    {
        public string acCode { get; set; }
        public string reference { get; set; }
        public string details { get; set; }
        public decimal unpaid { get; set; }
        public decimal totalNet { get; set; }
        public decimal totalVAT { get; set; }
        public long vatTypeId { get; set; }
        public long customerId { get; set; }
    }
}

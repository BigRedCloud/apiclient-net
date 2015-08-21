namespace BigRedCloud.Api.Model
{
    public class ProductDto : BaseApiDto
    {
        public long id { get; set; }
        public decimal unitPrice { get; set; }
        public string stockCode { get; set; }
        public bool grossUnitPrice { get; set; }
        public bool hasDefaultVatRate { get; set; }
        public long? vatRateId { get; set; }
        public string[] details { get; set; }
        public byte[] timestamp { get; set; }
    }
}

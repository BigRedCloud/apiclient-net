namespace BigRedCloud.Api.Model
{
    public class VatEntryDto : BaseApiDto
    {
        public long id { get; set; }
        public long vatRateId { get; set; }
        public decimal percentage { get; set; }
        public decimal amount { get; set; }
    }
}

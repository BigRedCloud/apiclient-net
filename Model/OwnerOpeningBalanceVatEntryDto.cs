namespace BigRedCloud.Api.Model
{
    public class OwnerOpeningBalanceVatEntryDto : BaseApiDto
    {
        public long vatRateId { get; set; }
        public decimal amount { get; set; }
    }
}

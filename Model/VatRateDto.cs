namespace BigRedCloud.Api.Model
{
    public class VatRateDto : BaseApiDto
    {
        public long id { get; set; }
        public decimal percentage { get; set; }
        public int orderIndex { get; set; }
        public bool isActive { get; set; }
        public bool isDefault { get; set; }
        public long vatCategoryId { get; set; }
    }
}

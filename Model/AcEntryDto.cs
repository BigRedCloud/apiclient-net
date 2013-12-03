namespace BigRedCloud.Api.Model
{
    public class AcEntryDto : BaseApiDto
    {
        public long id { get; set; }
        public string accountCode { get; set; }
        public long analysisCategoryId { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
    }
}

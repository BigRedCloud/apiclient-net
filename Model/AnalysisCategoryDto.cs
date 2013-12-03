namespace BigRedCloud.Api.Model
{
    public class AnalysisCategoryDto : BaseApiDto
    {
        public long id { get; set; }
        public string description { get; set; }
        public int orderIndex { get; set; }
        public long categoryTypeId { get; set; }
        public long accountId { get; set; }
        public string accountCode { get; set; }
    }
}

namespace BigRedCloud.Api.Model
{
    public class EFTBankDto : BaseApiDto
    {
        public long id { get; set; }
        public string name { get; set; }
        public string branch { get; set; }
        public string sortCode { get; set; }
    }
}

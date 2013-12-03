namespace BigRedCloud.Api.Model
{
    public class AccountDto : BaseApiDto
    {
        public long id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string accountGroup { get; set; }
        public string accountType { get; set; }
    }
}

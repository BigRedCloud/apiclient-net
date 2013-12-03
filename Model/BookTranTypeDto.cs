namespace BigRedCloud.Api.Model
{
    public class BookTranTypeDto : BaseApiDto
    {
        public long id { get; set; }
        public string description { get; set; }
        public string code { get; set; }
    }
}

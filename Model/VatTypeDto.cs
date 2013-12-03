namespace BigRedCloud.Api.Model
{
    public class VatTypeDto : BaseApiDto
    {
        public long id { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public bool isOnlyZero { get; set; }
        public bool isNotApplicable { get; set; }
    }
}

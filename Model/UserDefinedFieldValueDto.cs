namespace BigRedCloud.Api.Model
{
    public class UserDefinedFieldValueDto : BaseApiDto
    {
        public long id { get; set; }
        public long userDefinedFieldId { get; set; }
        public string description { get; set; }
        public string value { get; set; }
    }
}

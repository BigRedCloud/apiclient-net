namespace BigRedCloud.Api.Model
{
    public class UserDefinedFieldDto : BaseApiDto
    {
        public long id { get; set; }
        public string description { get; set; }
        public int orderIndex { get; set; }
        public long categoryTypeId { get; set; }
    }
}

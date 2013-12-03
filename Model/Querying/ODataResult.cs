namespace BigRedCloud.Api.Model.Querying
{
    public class ODataResult<TApiDto>
    {
        public TApiDto[] Items { get; set; }
        public int? Count { get; set; }
        public string NextPageLink { get; set; }
    }
}

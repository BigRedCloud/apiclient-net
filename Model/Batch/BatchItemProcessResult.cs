namespace BigRedCloud.Api.Model.Batch
{
    public class BatchItemProcessResult<TApiDto>
    {
        public int code { get; set; }
        public string message { get; set; }
        public long id { get; set; }
        public TApiDto Item { get; set; }
    }
}

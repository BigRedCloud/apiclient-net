namespace BigRedCloud.Api.Model.Batch
{
    public class BatchItem<TApiDto>
    {
        public int opCode { get; set; }
        public TApiDto item { get; set; }
    }
}

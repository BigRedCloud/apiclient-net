using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Batch;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseCrudApiClient<TApiDto> : BaseQueryableApiClient<TApiDto>
        where TApiDto : BaseApiDto
    {
        protected BaseCrudApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName) { }

        public virtual TApiDto Get(long id)
        {
            return GetByApi<TApiDto>(id);
        }

        public virtual long Create(TApiDto apiDto)
        {
            return PostByApi(apiDto);
        }

        public virtual void Update(long id, TApiDto apiDto)
        {
            PutByApi(id, apiDto);
        }

        public virtual void Delete(long id, byte[] timestamp)
        {
            DeleteByApi(id, timestamp);
        }

        public virtual BatchItemProcessResult[] ProcessBatch(BatchItem<TApiDto>[] batchItems)
        {
            return BatchByApi(batchItems);
        }
    }
}

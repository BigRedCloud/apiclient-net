using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;
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
            TApiDto result = GetAsync(id).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<TApiDto> GetAsync(long id)
        {
            return await GetByApiAsync<TApiDto>(id).ConfigureAwait(false);
        }

        public virtual long Create(TApiDto apiDto)
        {
            long result = CreateAsync(apiDto).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<long> CreateAsync(TApiDto apiDto)
        {
            return await PostByApiAsync(apiDto).ConfigureAwait(false);
        }

        public virtual void Update(long id, TApiDto apiDto)
        {
            UpdateAsync(id, apiDto).WaitAndUnwrapException();
        }

        public virtual async Task UpdateAsync(long id, TApiDto apiDto)
        {
            await PutByApiAsync(id, apiDto).ConfigureAwait(false);
        }

        public virtual void Delete(long id, byte[] timestamp)
        {
            DeleteAsync(id, timestamp).WaitAndUnwrapException();
        }

        public virtual async Task DeleteAsync(long id, byte[] timestamp)
        {
            await DeleteByApiAsync(id, timestamp).ConfigureAwait(false);
        }

        public virtual BatchItemProcessResult<TApiDto>[] ProcessBatch(BatchItem<TApiDto>[] batchItems)
        {
            return ProcessBatchAsync(batchItems).ResultAndUnwrapException();
        }

        public virtual async Task<BatchItemProcessResult<TApiDto>[]> ProcessBatchAsync(BatchItem<TApiDto>[] batchItems)
        {
            return await BatchByApiAsync(batchItems).ConfigureAwait(false);
        }
    }
}

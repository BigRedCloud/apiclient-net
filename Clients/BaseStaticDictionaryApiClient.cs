using System;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;
using BigRedCloud.Api.Model;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseStaticDictionaryApiClient<TApiDto> : BaseQueryableApiClient<TApiDto>
        where TApiDto : BaseApiDto
    {
        // Thread-safe lazy initialization.
        private readonly Lazy<Task<TApiDto[]>> _items;

        protected BaseStaticDictionaryApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName)
        {
            _items = new Lazy<Task<TApiDto[]>>(GetAllInternalAsync);
        }

        public override TApiDto[] GetAll(string odataParameters = null)
        {
            var result = GetAllAsync(odataParameters).ResultAndUnwrapException();
            return result;
        }

        public async override Task<TApiDto[]> GetAllAsync(string odataParameters = null)
        {
            return await _items.Value.ConfigureAwait(false);
        }

        private async Task<TApiDto[]> GetAllInternalAsync()
        {
            return await GetAllInternalAsync(null).ConfigureAwait(false);
        }
    }
}

using BigRedCloud.Api.Model.Querying;
using System;
using System.Collections.Generic;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseStaticDictionaryApiClient<TApiDto> : BaseApiClient
    {
        // Thread-safe lazy initialization.
        private readonly Lazy<TApiDto[]> _items;

        protected BaseStaticDictionaryApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName)
        {
            _items = new Lazy<TApiDto[]>(GetAllInternal);
        }

        public virtual TApiDto[] GetAll()
        {
            return _items.Value;
        }

        private TApiDto[] GetAllInternal()
        {
            ODataResult<TApiDto> pageResult = GetPageByApi<TApiDto>();
            List<TApiDto> result = new List<TApiDto>(pageResult.Items);

            while (pageResult.NextPageLink != null)
            {
                string requestParams = new Uri(pageResult.NextPageLink).Query.TrimStart('?');
                pageResult = GetPageByApi<TApiDto>(requestParams);
                result.AddRange(pageResult.Items);
            }

            return result.ToArray();
        }
    }
}

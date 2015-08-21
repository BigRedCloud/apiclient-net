using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;
using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseQueryableApiClient<TApiDto> : BaseApiClient
        where TApiDto : BaseApiDto
    {
        protected BaseQueryableApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName) { }

        public virtual TApiDto[] GetAll(string odataParameters = null)
        {
            TApiDto[] result = GetAllAsync(odataParameters).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<TApiDto[]> GetAllAsync(string odataParameters = null)
        {
            return await GetAllInternalAsync(odataParameters).ConfigureAwait(false);
        }

        public virtual ODataResult<TApiDto> GetPage(string odataParameters = null)
        {
            ODataResult<TApiDto> result = GetPageAsync(odataParameters).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<ODataResult<TApiDto>> GetPageAsync(string odataParameters = null)
        {
            return await GetPageByApiAsync<TApiDto>(odataParameters).ConfigureAwait(false);
        }

        protected async Task<TApiDto[]> GetAllInternalAsync(string odataParameters)
        {
            ODataResult<TApiDto> pageResult = await GetPageByApiAsync<TApiDto>(odataParameters).ConfigureAwait(false);
            List<TApiDto> result = new List<TApiDto>(pageResult.Items);

            while (pageResult.NextPageLink != null)
            {
                string requestParams = new Uri(pageResult.NextPageLink).Query.TrimStart('?');
                pageResult = await GetPageByApiAsync<TApiDto>(requestParams).ConfigureAwait(false);
                result.AddRange(pageResult.Items);
            }

            return result.ToArray();
        }
    }
}

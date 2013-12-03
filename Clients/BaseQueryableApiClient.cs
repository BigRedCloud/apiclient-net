using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseQueryableApiClient<TApiDto> : BaseApiClient
        where TApiDto : BaseApiDto
    {
        protected BaseQueryableApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName) { }

        public virtual ODataResult<TApiDto> GetPage(string odataParameters = null)
        {
            return GetPageByApi<TApiDto>(odataParameters);
        }
    }
}

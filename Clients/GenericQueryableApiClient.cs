using BigRedCloud.Api.Model;

namespace BigRedCloud.Api.Clients
{
    public class GenericQueryableApiClient<TApiDto> : BaseQueryableApiClient<TApiDto>
        where TApiDto : BaseApiDto
    {
        internal GenericQueryableApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName) { }
    }
}

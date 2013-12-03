using BigRedCloud.Api.Model;

namespace BigRedCloud.Api.Clients
{
    public class GenericCrudApiClient<TApiDto> : BaseCrudApiClient<TApiDto>
        where TApiDto : BaseApiDto
    {
        internal GenericCrudApiClient(string apiKey, string entitiesName) : base(apiKey, entitiesName) { }
    }
}

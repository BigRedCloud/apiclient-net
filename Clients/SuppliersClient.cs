using BigRedCloud.Api.Model;

namespace BigRedCloud.Api.Clients
{
    public class SuppliersClient : BaseOwnersClient<SupplierDto>
    {
        private const string EntitiesName = "Suppliers";

        internal SuppliersClient(string apiKey) : base(apiKey, EntitiesName) { }   
    }
}

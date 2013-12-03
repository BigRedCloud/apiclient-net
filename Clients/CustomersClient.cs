using BigRedCloud.Api.Model;

namespace BigRedCloud.Api.Clients
{
    public class CustomersClient : BaseOwnersClient<CustomerDto>
    {
        private const string EntitiesName = "Customers";

        internal CustomersClient(string apiKey) : base(apiKey, EntitiesName) { }   
    }
}

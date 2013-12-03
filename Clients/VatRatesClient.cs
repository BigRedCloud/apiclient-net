using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;

namespace BigRedCloud.Api.Clients
{
    public class VatRatesClient : BaseQueryableApiClient<VatRateDto>
    {
        private const string EntitiesName = "VatRates";

        internal VatRatesClient(string apiKey) : base(apiKey, EntitiesName) { }

        public ODataResult<VatRateDto> GetPageByVatCategory(long vatCategoryId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=vatCategoryId eq {0}&$orderby=orderIndex", vatCategoryId);
            ODataResult<VatRateDto> vatRates = GetPageByApi<VatRateDto>(odataParameters);
            return vatRates;
        }
    }
}

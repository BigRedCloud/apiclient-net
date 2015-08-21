using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class VatRatesClient : BaseQueryableApiClient<VatRateDto>
    {
        private const string EntitiesName = "VatRates";

        internal VatRatesClient(string apiKey) : base(apiKey, EntitiesName) { }

        public VatRateDto[] GetAllByVatCategory(long vatCategoryId)
        {
            VatRateDto[] result = GetAllByVatCategoryAsync(vatCategoryId).ResultAndUnwrapException();
            return result;
        }

        public async Task<VatRateDto[]> GetAllByVatCategoryAsync(long vatCategoryId)
        {
            string odataParameters = String.Format("$filter=vatCategoryId eq {0}&$orderby=orderIndex", vatCategoryId);
            return await GetAllAsync(odataParameters).ConfigureAwait(false);
        }

        public ODataResult<VatRateDto> GetPageByVatCategory(long vatCategoryId)
        {
            ODataResult<VatRateDto> result = GetPageByVatCategoryAsync(vatCategoryId).ResultAndUnwrapException();
            return result;
        }

        public async Task<ODataResult<VatRateDto>> GetPageByVatCategoryAsync(long vatCategoryId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=vatCategoryId eq {0}&$orderby=orderIndex", vatCategoryId);
            return await GetPageAsync(odataParameters).ConfigureAwait(false);
        }
    }
}

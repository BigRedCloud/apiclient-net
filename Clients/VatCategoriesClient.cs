using BigRedCloud.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class VatCategoriesClient : BaseStaticDictionaryApiClient<VatCategoryDto>
    {
        private const string EntitiesName = "VatCategories";

        // Thread-safe lazy initialization.
        private readonly Lazy<Dictionary<string, VatCategoryDto>> _vatCategoriesByDescription;

        internal VatCategoriesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _vatCategoriesByDescription = new Lazy<Dictionary<string, VatCategoryDto>>(GetAllGroupedByDescription);
        }

        public void ProcessVatRates(VatRatesByVatCategoryDto[] vatRates)
        {
            ProcessVatRatesAsync(vatRates).WaitAndUnwrapException();
        }

        public async Task ProcessVatRatesAsync(VatRatesByVatCategoryDto[] vatRates)
        {
            using (HttpResponseMessage response = await ProcessVatRatesRawAsync(vatRates).ConfigureAwait(false))
            {
                await EnsureSuccessAsync(response).ConfigureAwait(false);
            }
        }


        #region Specific Vat Categories

        public VatCategoryDto PurchasesForResale
        {
            get { return GetByDescription("Purchases For Resale"); }
        }

        public VatCategoryDto PurchasesNotForResale
        {
            get { return GetByDescription("Purchases Not For Resale"); }
        }

        public VatCategoryDto Sales
        {
            get { return GetByDescription("Sales"); }
        }

        #endregion Specific Vat Categories


        #region Private methods

        private VatCategoryDto GetByDescription(string description)
        {
            Dictionary<string, VatCategoryDto> vatCategoriesByDescription = _vatCategoriesByDescription.Value;
            return vatCategoriesByDescription[description];
        }

        private Dictionary<string, VatCategoryDto> GetAllGroupedByDescription()
        {
            VatCategoryDto[] vatCategories = GetAllAsync().ResultAndUnwrapException();
            return vatCategories.ToDictionary(vatCategory => vatCategory.description);
        }

        private async Task<HttpResponseMessage> ProcessVatRatesRawAsync(VatRatesByVatCategoryDto[] vatRates)
        {
            string requestUri = String.Format("{0}/vatRates", EntitiesName);
            return await SendToApiRawAsync(HttpMethod.Post, requestUri, vatRates).ConfigureAwait(false);
        }

        #endregion Private methods
    }
}

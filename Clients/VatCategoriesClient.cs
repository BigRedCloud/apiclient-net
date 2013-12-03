using BigRedCloud.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return _vatCategoriesByDescription.Value[description];
        }

        private Dictionary<string, VatCategoryDto> GetAllGroupedByDescription()
        {
            return GetAll().ToDictionary(vatCategory => vatCategory.description);
        }

        #endregion Private methods
    }
}

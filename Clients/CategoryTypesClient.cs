using System;
using BigRedCloud.Api.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class CategoryTypesClient : BaseQueryableApiClient<CategoryTypeDto>
    {
        private const string EntitiesName = "CategoryTypes";

        private const string CashReceiptsDescription = "Cash Receipts";
        private const string CashPaymentsDescription = "Cash Payments";
        private const string SalesDescription = "Sales";
        private const string PurchasesDescription = "Purchases";

        // Thread-safe lazy initialization.
        private readonly Lazy<Dictionary<string, CategoryTypeDto>> _staticCategoryTypesByDescription;

        internal CategoryTypesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _staticCategoryTypesByDescription = new Lazy<Dictionary<string, CategoryTypeDto>>(GetStaticGroupedByDescription);
        }


        #region Static Category Types

        public CategoryTypeDto CashReceipts
        {
            get { return GetStaticByDescription(CashReceiptsDescription); }
        }

        public CategoryTypeDto CashPayments
        {
            get { return GetStaticByDescription(CashPaymentsDescription); }
        }

        public CategoryTypeDto Sales
        {
            get { return GetStaticByDescription(SalesDescription); }
        }

        public CategoryTypeDto Purchases
        {
            get { return GetStaticByDescription(PurchasesDescription); }
        }

        #endregion Static Category Types


        #region Private methods

        private CategoryTypeDto GetStaticByDescription(string description)
        {
            Dictionary<string, CategoryTypeDto> staticCategoryTypesByDescription = _staticCategoryTypesByDescription.Value;
            return staticCategoryTypesByDescription[description];
        }

        private Dictionary<string, CategoryTypeDto> GetStaticGroupedByDescription()
        {
            IEnumerable<CategoryTypeDto> staticCategoryTypes = GetStaticCategoryTypesAsync().ResultAndUnwrapException();
            return staticCategoryTypes.ToDictionary(categoryType => categoryType.description);
        }

        private async Task<IEnumerable<CategoryTypeDto>> GetStaticCategoryTypesAsync()
        {
            string[] descriptionsOfStaticCategoryTypes = new string[]
            {
                CashReceiptsDescription, 
                CashPaymentsDescription, 
                SalesDescription, 
                PurchasesDescription
            };
            CategoryTypeDto[] categoryTypes = await GetAllAsync("$orderby=id").ConfigureAwait(false);
            return categoryTypes.Where(categoryType => descriptionsOfStaticCategoryTypes.Contains(categoryType.description));
        }

        #endregion Private methods
    }
}

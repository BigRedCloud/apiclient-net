using System;
using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System.Collections.Generic;
using System.Linq;

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
        private readonly Lazy<Dictionary<string, CategoryTypeDto>> _staticCategoryTypes;

        internal CategoryTypesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _staticCategoryTypes = new Lazy<Dictionary<string, CategoryTypeDto>>(GetStaticGroupedByDescription);
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
            return _staticCategoryTypes.Value[description];
        }

        private Dictionary<string, CategoryTypeDto> GetStaticGroupedByDescription()
        {
            return GetStaticCategoryTypes().ToDictionary(categoryType => categoryType.description);
        }

        private IEnumerable<CategoryTypeDto> GetStaticCategoryTypes()
        {
            string[] descriptionsOfStaticCategoryTypes = new string[]
            {
                CashReceiptsDescription, 
                CashPaymentsDescription, 
                SalesDescription, 
                PurchasesDescription
            };
            ODataResult<CategoryTypeDto> categoryTypes = GetPageByApi<CategoryTypeDto>("$orderby=id");
            return categoryTypes.Items.Where(categoryType => descriptionsOfStaticCategoryTypes.Contains(categoryType.description));
        }

        #endregion Private methods
    }
}

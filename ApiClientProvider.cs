using BigRedCloud.Api.Clients;
using BigRedCloud.Api.Configuration;
using BigRedCloud.Api.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BigRedCloud.Api
{
    public class ApiClientProvider
    {
        #region API entities' names

        private const string AccountsName = "Accounts";
        private const string AnalysisCategoriesName = "AnalysisCategories";
        private const string BankAccountsName = "BankAccounts";
        private const string BookTranTypesName = "BookTranTypes";
        private const string CategoryTypesName = "CategoryTypes";
        private const string CashReceiptsName = "CashReceipts";
        private const string CustomersName = "Customers";
        private const string PaymentsName = "Payments";
        private const string ProductsName = "Products";
        private const string PurchasesName = "Purchases";
        private const string SalesName = "Sales";
        private const string SalesCreditNotesName = "SalesCreditNotes";
        private const string SalesEntriesName = "SalesEntries";
        private const string SalesInvoicesName = "SalesInvoices";
        private const string SuppliersName = "Suppliers";
        private const string UserDefinedFieldsName = "UserDefinedFields";
        private const string VatCategoriesName = "VatCategories";
        private const string VatRatesName = "VatRates";
        private const string VatTypesName = "VatTypes";

        #endregion API entities' names

        // Thread-safe lazy initialization.
        private static Dictionary<string, Lazy<ApiClientProvider>> _apiClientProviders;
        private static string _defaultApiKeyName;

        private readonly Dictionary<string, object> _apiClients;

        static ApiClientProvider()
        {
            InitializeApiClientProviders();
        }

        private ApiClientProvider(string apiKey)
        {
            _apiClients = GetApiClients(apiKey);
        }


        #region Public static methods

        public static ApiClientProvider Default
        {
            get
            {
                if (String.IsNullOrEmpty(_defaultApiKeyName))
                {
                    throw new InvalidOperationException("Default Api Key is not specified.");
                }
                return _apiClientProviders[_defaultApiKeyName].Value;
            }
        }

        public static ApiClientProvider For(string apiKeyName)
        {
            Lazy<ApiClientProvider> lazyProvider;
            if (!_apiClientProviders.TryGetValue(apiKeyName, out lazyProvider))
            {
                string message = String.Format("Api Key with name {0} is not specified.", apiKeyName);
                throw new InvalidOperationException(message);
            }
            return lazyProvider.Value;
        }

        public static ApiClientProvider New(string apiKey)
        {
            return new ApiClientProvider(apiKey);
        }

        #endregion Public static methods


        #region API clients

        public GenericQueryableApiClient<AccountDto> Accounts
        {
            get { return GetSpecificClient<GenericQueryableApiClient<AccountDto>>(AccountsName); }
        }

        public AnalysisCategoriesClient AnalysisCategories
        {
            get { return GetSpecificClient<AnalysisCategoriesClient>(AnalysisCategoriesName); }
        }

        public GenericCrudApiClient<BankAccountDto> BankAccounts
        {
            get { return GetSpecificClient<GenericCrudApiClient<BankAccountDto>>(BankAccountsName); }
        }

        public BookTranTypesClient BookTranTypes
        {
            get { return GetSpecificClient<BookTranTypesClient>(BookTranTypesName); }
        }

        public CategoryTypesClient CategoryTypes
        {
            get { return GetSpecificClient<CategoryTypesClient>(CategoryTypesName); }
        }

        public GenericCrudApiClient<CashReceiptDto> CashReceipts
        {
            get { return GetSpecificClient<GenericCrudApiClient<CashReceiptDto>>(CashReceiptsName); }
        }

        public CustomersClient Customers
        {
            get { return GetSpecificClient<CustomersClient>(CustomersName); }
        }

        public GenericCrudApiClient<PaymentDto> Payments
        {
            get { return GetSpecificClient<GenericCrudApiClient<PaymentDto>>(PaymentsName); }
        }

        public GenericCrudApiClient<ProductDto> Products
        {
            get { return GetSpecificClient<GenericCrudApiClient<ProductDto>>(ProductsName); }
        }

        public GenericCrudApiClient<PurchaseDto> Purchases
        {
            get { return GetSpecificClient<GenericCrudApiClient<PurchaseDto>>(PurchasesName); }
        }

        public GenericQueryableApiClient<SalesDto> Sales
        {
            get { return GetSpecificClient<GenericQueryableApiClient<SalesDto>>(SalesName); }
        }

        public GenericCrudApiClient<SalesInvoiceCreditNoteDto> SalesCreditNotes
        {
            get { return GetSpecificClient<GenericCrudApiClient<SalesInvoiceCreditNoteDto>>(SalesCreditNotesName); }
        }

        public GenericCrudApiClient<SalesEntryDto> SalesEntries
        {
            get { return GetSpecificClient<GenericCrudApiClient<SalesEntryDto>>(SalesEntriesName); }
        }

        public GenericCrudApiClient<SalesInvoiceCreditNoteDto> SalesInvoices
        {
            get { return GetSpecificClient<GenericCrudApiClient<SalesInvoiceCreditNoteDto>>(SalesInvoicesName); }
        }

        public SuppliersClient Suppliers
        {
            get { return GetSpecificClient<SuppliersClient>(SuppliersName); }
        }

        public UserDefinedFieldsClient UserDefinedFields
        {
            get { return GetSpecificClient<UserDefinedFieldsClient>(UserDefinedFieldsName); }
        }

        public VatCategoriesClient VatCategories
        {
            get { return GetSpecificClient<VatCategoriesClient>(VatCategoriesName); }
        }

        public VatRatesClient VatRates
        {
            get { return GetSpecificClient<VatRatesClient>(VatRatesName); }
        }

        public VatTypesClient VatTypes
        {
            get { return GetSpecificClient<VatTypesClient>(VatTypesName); }
        }

        #endregion API clients


        #region Private methods

        private GenericQueryableApiClient<TApiDto> CreateQueryableClient<TApiDto>(string apiKey, string entitiesName) where TApiDto : BaseApiDto
        {
            return new GenericQueryableApiClient<TApiDto>(apiKey, entitiesName);
        }

        private GenericCrudApiClient<TApiDto> CreateCrudClient<TApiDto>(string apiKey, string entitiesName) where TApiDto : BaseApiDto
        {
            return new GenericCrudApiClient<TApiDto>(apiKey, entitiesName);
        }

        private TApiClient GetSpecificClient<TApiClient>(string entitiesName)
        {
            return (TApiClient)_apiClients[entitiesName];
        }

        private static void InitializeApiClientProviders()
        {
            BigRedCloudApiSection apiConfigSection = (BigRedCloudApiSection)ConfigurationManager.GetSection("bigRedCloudApiSection");
            ApiKeyElementCollection apiKeysConfig = apiConfigSection != null ? apiConfigSection.ApiKeys : new ApiKeyElementCollection();

            ApiKeyElement defaultApiKeyConfig = apiKeysConfig.Cast<ApiKeyElement>().FirstOrDefault(apiKeyConfig => apiKeyConfig.IsDefault);
            _defaultApiKeyName = defaultApiKeyConfig != null ? defaultApiKeyConfig.Name : null;

            _apiClientProviders = new Dictionary<string, Lazy<ApiClientProvider>>(apiKeysConfig.Count);
            foreach (ApiKeyElement apiKeyConfig in apiKeysConfig)
            {
                ApiKeyElement localApiKeyConfig = apiKeyConfig;
                _apiClientProviders[localApiKeyConfig.Name] = new Lazy<ApiClientProvider>(() => new ApiClientProvider(localApiKeyConfig.Value));
            }
        }

        private Dictionary<string, object> GetApiClients(string apiKey)
        {
            return new Dictionary<string, object>
            {
                { AccountsName, CreateQueryableClient<AccountDto>(apiKey, AccountsName) },
                { AnalysisCategoriesName, new AnalysisCategoriesClient(apiKey) },
                { BankAccountsName, CreateCrudClient<BankAccountDto>(apiKey, BankAccountsName) },
                { BookTranTypesName, new BookTranTypesClient(apiKey) },
                { CashReceiptsName, CreateCrudClient<CashReceiptDto>(apiKey, CashReceiptsName) },
                { CategoryTypesName, new CategoryTypesClient(apiKey) },
                { CustomersName, new CustomersClient(apiKey) },
                { PaymentsName, CreateCrudClient<PaymentDto>(apiKey, PaymentsName) },
                { ProductsName, CreateCrudClient<ProductDto>(apiKey, ProductsName) },
                { PurchasesName, CreateCrudClient<PurchaseDto>(apiKey, PurchasesName) },
                { SalesName, CreateQueryableClient<SalesDto>(apiKey, SalesName) },
                { SalesCreditNotesName, CreateCrudClient<SalesInvoiceCreditNoteDto>(apiKey, SalesCreditNotesName) },
                { SalesEntriesName, CreateCrudClient<SalesEntryDto>(apiKey, SalesEntriesName) },
                { SalesInvoicesName, CreateCrudClient<SalesInvoiceCreditNoteDto>(apiKey, SalesInvoicesName) },
                { SuppliersName, new SuppliersClient(apiKey) },
                { UserDefinedFieldsName, new UserDefinedFieldsClient(apiKey) },
                { VatCategoriesName, new VatCategoriesClient(apiKey) },
                { VatRatesName, new VatRatesClient(apiKey) },
                { VatTypesName, new VatTypesClient(apiKey) }
            };
        }

        #endregion Private methods
    }
}

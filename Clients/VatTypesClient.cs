using System;
using BigRedCloud.Api.Model;
using System.Collections.Generic;
using System.Linq;

namespace BigRedCloud.Api.Clients
{
    public class VatTypesClient : BaseStaticDictionaryApiClient<VatTypeDto>
    {
        private const string EntitiesName = "VatTypes";

        // Thread-safe lazy initialization.
        private readonly Lazy<Dictionary<string, VatTypeDto>> _vatTypesByDescription;

        internal VatTypesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _vatTypesByDescription = new Lazy<Dictionary<string, VatTypeDto>>(GetAllGroupedByDescription);
        }


        #region Specific Vat Types

        public VatTypeDto Domestic
        {
            get { return GetByDescription("Domestic"); }
        }

        public VatTypeDto OtherEU
        {
            get { return GetByDescription("Other EU"); }
        }

        public VatTypeDto ForeignNonEU
        {
            get { return GetByDescription("Foreign - Non EU"); }
        }

        public VatTypeDto VatExempt
        {
            get { return GetByDescription("VAT Exempt"); }
        }

        public VatTypeDto NotApplicable
        {
            get { return GetByDescription("Not Applicable"); }
        }

        public VatTypeDto ReverseCharge
        {
            get { return GetByDescription("Reverse Charge"); }
        }

        #endregion Specific Vat Types


        #region Private methods

        private VatTypeDto GetByDescription(string description)
        {
            return _vatTypesByDescription.Value[description];
        }

        private Dictionary<string, VatTypeDto> GetAllGroupedByDescription()
        {
            return GetAll().ToDictionary(vatType => vatType.description);
        }

        #endregion Private methods
    }
}

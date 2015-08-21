using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class ProductTranDto : BaseApiDto
    {
        public long id { get; set; }
        public decimal amount { get; set; }
        public decimal amountNet { get; set; }
        public decimal percentage { get; set; }
        public long? productId { get; set; }
        public string productCode { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal vat { get; set; }
        public long vatRateId { get; set; }
        public IEnumerable<string> tranNotes { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
    }
}

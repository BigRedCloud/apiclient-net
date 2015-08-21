using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class PurchaseDto : BaseBookTranDto
    {
        public string reference { get; set; }
        public long supplierId { get; set; }
        public decimal totalNet { get; set; }
        public decimal totalVAT { get; set; }
        public long vatTypeId { get; set; }
        public decimal unallocated { get; set; }
        public IEnumerable<string> detailCollection { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
        public IEnumerable<VatEntryDto> vatEntries { get; set; }
    }
}

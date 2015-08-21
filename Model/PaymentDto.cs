using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class PaymentDto : BaseBookTranDto
    {
        public long bankAccountId { get; set; }
        public string bankAccountCode { get; set; }
        public string reference { get; set; }
        public long? supplierId { get; set; }
        public long? transferBankId { get; set; }
        public string transferBankCode { get; set; }
        public decimal discount { get; set; }
        public decimal unallocated { get; set; }
        public IEnumerable<string> detailCollection { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
    }
}

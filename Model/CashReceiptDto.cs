using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class CashReceiptDto : BaseCashDto
    {
        public string acCode { get; set; }
        public decimal discount { get; set; }
        public decimal unallocated { get; set; }
        public long? customerId { get; set; }
        public IEnumerable<NoteDto> detailCollection { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
        public IEnumerable<VatEntryDto> vatEntries { get; set; }
    }
}

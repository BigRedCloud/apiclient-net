﻿using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class CashReceiptDto : BaseCashDto
    {
        public decimal discount { get; set; }
        public decimal unallocated { get; set; }
        public long? customerId { get; set; }
        public IEnumerable<string> detailCollection { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
        public IEnumerable<VatEntryDto> vatEntries { get; set; }
    }
}

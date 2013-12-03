using System;
using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class OwnerOpeningBalanceDto : BaseApiDto
    {
        public long id { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime procDate { get; set; }
        public string reference { get; set; }
        public byte[] timestamp { get; set; }
        public decimal total { get; set; }
        public decimal totalVAT { get; set; }
        public decimal unpaid { get; set; }
        public IEnumerable<OwnerOpeningBalanceVatEntryDto> vatEntries { get; set; }

        public bool? isChanged { get; set; }
    }
}

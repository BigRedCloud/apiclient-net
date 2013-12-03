using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class SalesDto : BaseSalesDto
    {
        public string loType { get; set; }
        public IEnumerable<AcEntryDto> acEntries { get; set; }
        public IEnumerable<VatEntryDto> vatEntries { get; set; }
    }
}
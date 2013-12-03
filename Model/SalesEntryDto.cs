using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class SalesEntryDto : BaseSalesDto
    {
        public IEnumerable<AcEntryDto> acEntries { get; set; }
        public IEnumerable<VatEntryDto> vatEntries { get; set; }
    }
}

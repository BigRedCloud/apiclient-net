using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class VatRatesByVatCategoryDto : BaseApiDto
    {
        public long vatCategoryId { get; set; }
        public IEnumerable<VatRateDto> vatRates { get; set; }
    }
}

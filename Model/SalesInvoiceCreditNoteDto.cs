using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class SalesInvoiceCreditNoteDto : BaseSalesDto
    {
        public string ourReference { get; set; }
        public string yourReference { get; set; }
        public string loType { get; set; }
        public IEnumerable<NoteDto> deliveryTo { get; set; }
        public IEnumerable<ProductTranDto> productTrans { get; set; }
    }
}

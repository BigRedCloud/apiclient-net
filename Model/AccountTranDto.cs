using System;

namespace BigRedCloud.Api.Model
{
    public class AccountTranDto : BaseApiDto
    {
        public long id { get; set; }
        public string bookTransactionReference { get; set; }
        public string bookTypeDesc { get; set; }
        public decimal credit { get; set; }
        public decimal debit { get; set; }
        public DateTime procDate { get; set; }
    }
}

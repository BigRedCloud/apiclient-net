namespace BigRedCloud.Api.Model
{
    public abstract class BaseOwnerDto : BaseApiDto
    {
        public long id { get; set; }
        public string code { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string authCode { get; set; }
        public string contact { get; set; }
        public string eFTReference { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string ourCode { get; set; }
        public string phone { get; set; }
        public string vatReg { get; set; }
        public byte[] timestamp { get; set; }
        public string[] address { get; set; }
        public EFTBankDto bank { get; set; }
        
        public decimal? ledgerBalance { get; set; }
        public OwnerOpeningBalanceInPeriodsDto openingBalance { get; set; }
        public OwnerOpeningBalanceDto[] openingBalances { get; set; }
    }
}

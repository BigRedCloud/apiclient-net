namespace BigRedCloud.Api.Model
{
    public class OwnerOpeningBalanceInPeriodsDto : BaseApiDto
    {
        public decimal currentMonth { get; set; }
        public decimal oneMonthOld { get; set; }
        public decimal twoMonthsOld { get; set; }
        public decimal threeMonthsOld { get; set; }
    }
}

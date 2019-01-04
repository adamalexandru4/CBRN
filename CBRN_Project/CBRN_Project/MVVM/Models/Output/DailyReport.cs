namespace CBRN_Project.MVVM.Models
{
    public class DailyReport
    {
        public double NewKIA { get; set; } = 0;
        public double NewDOW { get; set; } = 0;
        public double NewFAT { get; set; } = 0;
        public double NewWIA { get; set; } = 0;
        public double NewRTD { get; set; } = 0;

        public double KIA   { get; set; } = 0;
        public double DOW   { get; set; } = 0;
        public double FAT   { get; set; } = 0;
        public double WIA1  { get; set; } = 0;
        public double WIA2  { get; set; } = 0;
        public double WIA3  { get; set; } = 0;
        public double WIA   { get; set; } = 0;
        public double RTD   { get; set; } = 0;

        public static DailyReport operator+ (DailyReport left, DailyReport right)
        {
            DailyReport temp = new DailyReport()
            {
                NewKIA = left.NewKIA + right.NewKIA,
                NewDOW = left.NewDOW + right.NewDOW,
                NewFAT = left.NewFAT + right.NewFAT,
                NewWIA = left.NewWIA + right.NewWIA,
                NewRTD = left.NewRTD + right.NewRTD,

                KIA  = left.KIA  + right.KIA,
                DOW  = left.DOW  + right.DOW,
                FAT  = left.FAT  + right.FAT,
                WIA1 = left.WIA1 + right.WIA1,
                WIA2 = left.WIA2 + right.WIA2,
                WIA3 = left.WIA3 + right.WIA3,
                WIA  = left.WIA  + right.WIA,
                RTD  = left.RTD  + right.RTD
            };

            return temp;
        }

        public void AddTotalsFrom(DailyReport right)
        {
            KIA  += right.KIA;
            DOW  += right.DOW;
            FAT  += right.FAT;
            WIA1 += right.WIA1;
            WIA2 += right.WIA2;
            WIA3 += right.WIA3;
            WIA  += right.WIA;
            RTD  += right.RTD;
        }
    }
}

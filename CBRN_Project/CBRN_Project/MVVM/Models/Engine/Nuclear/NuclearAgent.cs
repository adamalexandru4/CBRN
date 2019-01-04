using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBRN_Project.MVVM.Models.Engine.Radiological.

namespace CBRN_Project.MVVM.Models.Engine.Nuclear
{
    class NuclearAgent
    {
        #region Properties
        private static NuclearProperties NucProperties { get; set; }

        //Tables
        private static DataTable OutputTable { get; set; }


        #endregion

        #region Methods 
        private static void CalculateNucChallenge(Icon icon)
        {
            Init(icon);
            GenerateOutputTable();
            CalculateChallenge(icon);
        }

        public static void Init(Icon icon)
        {
            var result = icon.Challenges.Where(c => c.Agent.Equals("Nuclear")).FirstOrDefault();
            InitAPF(icon);
            NucProperties.NucBlast = result.Values[2] / NucProperties.APFBlast;
            NucProperties.NucWholeBody = result.Values[0] / NucProperties.APFN
                                            + result.Values[1] / NucProperties.APFY;
            NucProperties.NucThermal = CalculateThermalChallenge(icon, result.Values[3]);

        }

        public static void CalculateChallenge(Icon icon)
        {
            CalculateRange();
            
            List <string> wiaBlast = CalculateBlastWIA(NucProperties.RangeBl);
            List <string> wiaWb = CalculateWBWIA(NucProperties.RangeWB, true);
            List <string> wiaThermal = CalculateThermalWIA(NucProperties.RangeTh);

            InterpretState(wiaWb, wiaBlast, wiaThermal, icon);
        }

        public static void GenerateOutputTable()
        {
            CreateTable();
        }

        #endregion

        #region AuxMethods

        #region Init
      
        private static double CalculateThermalChallenge(Icon icon, double value)
        {
            double result = Math.Acos(310 / value) / Math.PI * 0.88 
                            + Math.Acos(109/ value) / Math.PI * 0.12;
            return result;
        } 

        private static void InitAPF(Icon icon)
        {
            NucProperties.APFBlast = icon.IPE.NucBlast;
            NucProperties.APFThermal = icon.Uniform.thresoldThermalFluence;
            NucProperties.APFN = icon.IPE.NeutronRad;
            NucProperties.APFY = icon.IPE.GammaRad;
        }
        #endregion

        #region Compute
        public static void CalculateRange()
        {
            NucProperties.RangeTh = CalculateThermalRange(NucProperties.NucThermal);
            NucProperties.RangeWB = CalculateWBRange(NucProperties.NucWholeBody);
            NucProperties.RangeBl = CalculateBlastRange(NucProperties.RangeBl);
        }

        private static int CalculateBlastRange(double dose)
        {
            if (TestRange(0, 50, dose))
                return 0;
            if (TestRange(50, 140, dose))
                return 1;
            if (TestRange(140, 240, dose))
                return 2;
            if (TestRange(240, 290, dose))
                return 3;
            if (TestRange(290, double.PositiveInfinity, dose))
                return 4;
            return -1;
        }

        private static int CalculateWBRange(double dose)
        {
            if (TestRange(0, 1.25, dose))
                return 0;
            if (TestRange(1.25, 3, dose))
                return 1;
            if (TestRange(3, 4.5, dose))
                return 2;
            if (TestRange(4.5, 6.8, dose))
                return 3;
            if (TestRange(6.8, 8.3, dose))
                return 4;
            if (TestRange(8.3, 8.5, dose))
                return 5;
            if (TestRange(8.5, double.PositiveInfinity, dose))
                return 6;
            return -1;
        }

        private static int CalculateThermalRange(double dose)
        {
            if (TestRange(0, 1, dose))
                return 0;
            if (TestRange(1, 10, dose))
                return 1;
            if (TestRange(10, 15, dose))
                return 2;
            if (TestRange(15, 20, dose))
                return 3;
            if (TestRange(20, 30, dose))
                return 4;
            if (TestRange(30, 45, dose))
                return 5;
            if (TestRange(45, double.PositiveInfinity, dose))
                return 6;
            return -1;
        }

        private static bool TestRange(double left, double right, double value)
        {
            if(left <= value && right > value)
                return true;
            return false;
        }

        private static double CalculateThermalChallengedPersonsNumber(Icon icon)
        {
            double result = icon.Personnel;
            //double fraction = icon.Vehicle_Shelter.BlastProtection.ProtectionFactor;
            return result;
        }

        private static List<string> CalculateBlastWIA(int range)
        {
            List<string> result = new List<string>();
            if(range == 1)
                return new List<string> { "WIA", "RTD", "9", "100"};
            if (range == 2)
                return new List<string> { "WIA", "RTD", "22", "100"};
            if (range == 3)
                return new List<string> { "WIA", "RTD", "30+", "100"};
            if (range == 4)
                return new List<string> { "WIA", "DOW", "2", "23", "CONV", "30+", "77"};
            else
                return new List<string> { "None" };
                
        }

        private static List<string> CalculateThermalWIA(int range)
        {
            List<string> result = new List<string>();
            if (range == 1 || range == 2)
                return new List<string> { "WIA", "RTD", "22", "100" };
            if (range == 3 || range == 4)
                return new List<string> { "WIA", "CONV", "30+", "50", "RTD", "30+", "50" };
            if (range == 5)
                return new List<string> { "WIA", "DOW", "9", "25", "CONV", "30+", "75" };
            if (range == 6)
                return new List<string> { "WIA", "DOW", "7", "75", "CONV", "30+", "25" };
            else
                return new List<string> { "None" };
        }

        private static List<string> CalculateWBWIA(int range, bool GCSF)
        {
            List<string> result = new List<string>();
            if(range == 0)
                return new List<string> { "None" };
            if (range == 1)
                return new List<string> { "WIA", "CONV", "2", "100" };
            if (GCSF)
            { 
                if (range == 2 || range == 3 || range == 4)
                    return new List<string> { "WIA", "CONV", "30", "100" };
                else
                    return new List<string> { "DOW"};
            }
            else
            {
                if (range == 2 || range == 3)
                    return new List<string> { "WIA", "CONV", "30", "100" };
                else
                    return new List<string> { "DOW" };
            }
        }

        private static void InterpretState(List<string> wbstat, List<string> blstat, List<string> thstat, Icon icon)
        {
            bool WB = true, Bl = true, Th = true;
            double procent = 1;
            if (wbstat.Contains("None") == true)
                WB = false;
            if (blstat.Contains("None") == true)
                Bl = false;
            if (thstat.Contains("None") == true)
                Th = false;
            if (WB == false && Bl == false && Th == false)
                return;

            if (Th == true)
                procent = CalculateThermalChallengedPersonsNumber(icon);

            List<int> newDOW = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> newWIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> newCONV = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> newRTD = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<int> newKIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (NucProperties.NucBlast > getThresholdBlast(10)) //need o yield default 10 KT
            {
                newKIA[0] += Convert.ToInt32(icon.Personnel);
                return;
            }

            if(wbstat.Contains("DOW"))
            {
                double time = 429 * Math.Pow(NucProperties.NucWholeBody, -1.3);
                int day = Convert.ToInt32(Math.Round(time));
                if (day > 1)
                    newWIA[0] += (int)icon.Personnel;
                newDOW[ConvertToTable(day)] += (int)icon.Personnel;
                return;
            }
            else
            {
                newWIA[0] += (int)icon.Personnel;
                if(thstat[1] == "DOW")
                {
                    int day = ConvertToTable(Convert.ToInt32(thstat[2]));
                    newDOW[day] += Convert.ToInt32(icon.Personnel * procent * Convert.ToInt32(thstat[3]) /100);
                    newCONV[8] += Convert.ToInt32(icon.Personnel * procent * Convert.ToInt32(thstat[6]) / 100);
                    return;
                }

                if(blstat[1] == "DOW")
                {
                    newWIA[0] += (int)icon.Personnel;
                    newDOW[2] += (int)(icon.Personnel * 0.23);
                    newCONV[9] += (int)(icon.Personnel * 0.77);
                    return;
                }

                if(wbstat[1] == "CONV" || thstat[1] == "CONV" )
                {
                    int day = Math.Max(Convert.ToInt32(wbstat[2]),Convert.ToInt32(thstat[2]));
                    newCONV[ConvertToTable(day)] += Convert.ToInt32(icon.Personnel);
                    return;
                }

                if(thstat[1] == "RTD" && blstat[1] == "RTD")
                {
                    int day = Math.Max(Convert.ToInt32(thstat[2]), Convert.ToInt32(blstat[2]));
                    newRTD[ConvertToTable(day)] += Convert.ToInt32(icon.Personnel);
                    return;
                }

                if (thstat[1] == "RTD")
                {
                    int day = Convert.ToInt32(thstat[2]);
                    newRTD[ConvertToTable(day)] += Convert.ToInt32(icon.Personnel);
                    return;
                }

                if (blstat[1] == "RTD")
                {
                    int day = Convert.ToInt32(blstat[2]);
                    newRTD[ConvertToTable(day)] += Convert.ToInt32(icon.Personnel);
                    return;
                }
            }
            CompletTable(newKIA, newDOW, newWIA, newCONV, newRTD);
        }

        private static void CompletTable(List<int> newKIA, List<int> newDOW, List<int> newWIA, List<int> newCONV, List<int> newRTD )
        {
            for(int i = 0; i < newKIA.Count(); i++)
            {
                OutputTable.Rows[0].SetField(i, OutputTable.Rows[0].Field());
                OutputTable.Rows[1].SetField(i, 1);
                OutputTable.Rows[2].SetField(i, 1);
                OutputTable.Rows[3].SetField(i, 1);
                OutputTable.Rows[4].SetField(i, 1);
                OutputTable.Rows[5].SetField(i, 1);
                OutputTable.Rows[6].SetField(i, 1);
                OutputTable.Rows[7].SetField(i, 1);
                OutputTable.Rows[8].SetField(i, 1);
                OutputTable.Rows[9].SetField(i, 1);
            }
        }

        private static int ConvertToTable(int day)
        {
            if (day > 7 && day < 15)
                return 7;
            if (day > 14 && day < 31)
                return 8;
            if (day > 30)
                return 9;
            else return day;
        }

        private static double getThresholdBlast(double yield)
        {
            if (yield <= 10)
                return -170.68 * Math.Log(yield) + 689.47;
            else
                return -56.89 * Math.Log(yield) + 427.47;
        }

        #endregion


        #region Output

        private static void CreateTable()
        {
            List<string> dailyDetails = new List<string>
                { "New KIA (N)", "New DOW (CRN)", "Sum of New Fatalities", "New WIA (Nuclear)", "New CONV (Nuclear)", "New RTD",
                    "Sum of Fatalities", "Sum of WIA", "Sum of Conv", "Sum of RTD"};
            OutputTable = Radiological.RadiologicalAgent.InitTable(dailyDetails);
        }
        #endregion


        #endregion
    }
}

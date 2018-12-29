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
        private static NuclearChallenge NucChallenge { get; set; }
        private static NuclearProperties NucProperties { get; set; }

        //Tables
        private static DataTable OutputTable { get; set; }


        #endregion

        #region Methods 
        private static void CalculateNucChallenge(Icon icon)
        {
            Init(icon);
            CalculateChallenge(icon);
            GenerateOutput();
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
            double procent = CalculateThermalChallengedPersonsNumber(icon);
        }

        public static void GenerateOutput()
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
            if (TestRange(15, 20, dose))\
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

        #endregion

        #region Output

        private static void CreateTable()
        {
            List<string> dailyDetails = new List<string>
                { "New KIA (N)", "New DOW (CRN)", "Sum of New Fatalities", "New WIA (Nuclear)", "New CONV (Nuclear)", "New RTD",
                    "Sum of Fatalities", "Sum of WIA", "Sum of Conv", "RTD"};
            OutputTable = Radiological.RadiologicalAgent.InitTable(dailyDetails);
        }
        #endregion


        #endregion
    }
}

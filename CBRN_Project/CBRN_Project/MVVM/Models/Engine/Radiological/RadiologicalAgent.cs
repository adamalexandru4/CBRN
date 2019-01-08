using CBRN_Project.MVVM.Models.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Engine.Radiological
{
    public class RadiologicalAgent
    {
        #region Properties

        //Class RadiologicalProperties contain required data to calculate challenge
        private static RadiologicalProperties RadChallengeProperties = new RadiologicalProperties();

        //Class derivated from EffChallenge 
        private static RadiologicalChallenge RadEffChallenge = new RadiologicalChallenge();
            
        //Injury Profile
        private static InjuryProfile WBInjuryProfile                    { get; set; }
        private static InjuryProfile CutInjuryProfile                   { get; set; }
        private static InjuryProfile CompositeInjuryProfile             { get; set; }
        private static bool Composite                                   { get; set; }
    

        //Tables
        private static DataTable OutputTable = new DataTable();
        
        #endregion


        #region Main Methods
       

        public static void CalculateChallenge (List<Icon> icons, List<string> izotop)
        {
            GenerateOutput();
            
            foreach (var icon in icons)
            {
                InitChallengeType(icon, izotop);

                Calculate(icon);
            }

        }

        private static void InitChallengeType (Icon icon, List<string> izotop)
        {
            //Verify if exist challenges for radiological agents
            if (icon.Challenges != null)
            {
                var results = icon.Challenges
                                .Where(c => c.Agent == "Radiological");

                //if exist convert to eff chalanges and complete the challenge type
                if (results.Count() > 0)
                {
                    RadChallengeProperties.ResetValue();
                    var RDDResult = results.Where(c => c.ChallengeType.Contains("RDD"));
                    if (RDDResult.Count() > 0)
                    {
                        RadChallengeProperties.Izotop = izotop;
                        foreach (var item in RDDResult)
                        {
                            ConvertToEffChallengeRDD(icon, item);
                        }
                    }
                    var FalloutResult = results.Where(c => c.ChallengeType.Contains("Fallout"));
                    foreach (var item in RDDResult)
                    {
                        ConvertToEffChallengeFallout(icon, item);
                    }
                }
            }

            //Verify if exist EffChallenge for radiological agent
            if (icon.EffChallenges != null)
            {
                RadEffChallenge.DoseWholeBody = icon.EffChallenges[0].Value;
                RadEffChallenge.DoseCoutanous = icon.EffChallenges[0].SecondValue;
            }
        }

        private static void Calculate(Icon icon)
        {
            #region Injury Profile
            //InitInjuryProfile();
            //CalculateInjuryProfile();
            //CalculateCompInjuryProfile();
            #endregion

            int wbrange = EstimateRangeWBDose();
            int cutrange = EstimateRangeCutDose();

            List<string> wbstate = GetStateWB(wbrange);
            List<string> cutstate = GetStateCut(cutrange);

            InterpretState(wbstate, cutstate, icon);
            CompletTable();
        }

        private static void GenerateOutput()
        {
            CreateTables();   
        }

        #endregion



        #region Auxiliar methods

        #region RDD Challange
        public static void ConvertToEffChallengeRDD(Icon icon, Challenge item)
        {
            double apf = getAPF(icon);
            double Zfactor;

            for (int i = 0; i < RadChallengeProperties.Izotop.Count(); i++)
            {
                Zfactor = GetRDDFactor("cloudshine", RadChallengeProperties.Izotop[i]);
                RadChallengeProperties.CutaneousCloudShine += ComputeChallenge(item, apf, Zfactor);
                RadChallengeProperties.WholeBodyCloudShine += ComputeChallenge(item, apf, Zfactor);

                Zfactor = GetRDDFactor("groundshine", RadChallengeProperties.Izotop[i]);
                RadChallengeProperties.WholeBodyGroundshine += ComputeChallenge(item, apf, Zfactor);
                RadChallengeProperties.CutaneousGroundShine += ComputeChallenge(item, apf, Zfactor);

                Zfactor = GetRDDFactor("skin", RadChallengeProperties.Izotop[i]);
                RadChallengeProperties.CutaneousSkin += ComputeChallenge(item, apf, Zfactor);

                Zfactor = GetRDDFactor("inhalation", RadChallengeProperties.Izotop[i]);
                RadChallengeProperties.WholeBodyInhalation += ComputeChallenge(item, apf, Zfactor);
            }

            RadEffChallenge.DoseCoutanous += RadChallengeProperties.GetCutaneousDose();
            RadEffChallenge.DoseWholeBody += RadChallengeProperties.GetWholeBodyDose();
        }

        private static double GetRDDFactor(string context, string izotop)
        {
            switch (context)
            {
                case "cloudshine":
                    return 1;
                case "groundshine":
                    return 1;
                case "inhalation":
                    return 1;
                case "skin":
                    return 1;

                default:
                    return 1;
            }

        }

        #endregion

        #region Fallout Challenge

        public static void ConvertToEffChallengeFallout(Icon icon, Challenge item)
        {
            double apf = getAPF(icon);
            double Zfactor = GetFalloutFactor("groundshine");
            RadChallengeProperties.WholeBodyGroundshineFallout += ComputeChallenge(item, apf, Zfactor);
            Zfactor = GetFalloutFactor("skin");
            RadChallengeProperties.CutaneousSkinFallout += ComputeChallenge(item, apf, Zfactor);


            RadEffChallenge.DoseWholeBody += RadChallengeProperties.WholeBodyGroundshineFallout;
            RadEffChallenge.DoseCoutanous += RadChallengeProperties.CutaneousSkinFallout
                                          + RadChallengeProperties.WholeBodyGroundshineFallout;
            
        }

        private static double GetFalloutFactor(string context)
        {
            switch (context)
            {
                case "groundshine":
                    return 1;
                case "skin":
                    return 1;
                default:
                    return 1;
            }
        }

        #endregion

        #region Auxiliar Challenge Methods
        public static double ComputeChallenge(Challenge item, double apf, double Zfactor)
        {
            var diff = item.Values.First() / apf;
            for (int i = 1; i < item.Values.Count(); i++)
            {
                diff += (item.Values[i] - item.Values[i - 1]) / apf;
            }
            var result = diff * Zfactor;

            return result;
        }

        public static double getAPF(Icon icon)
        {
            double VSHFact = icon.Vehicle_Shelter.RadiationProtection.BetaRadiation
                                    * icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.Inhalation
                                    * icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.PercLiq
                                    * icon.Vehicle_Shelter.InhalationPercutaneousVapourProtection.PercVap;
            double IPEFact = icon.IPE.Inhalation * icon.IPE.PervVap * icon.IPE.PercLiq * icon.IPE.BetaRad;
            double PropFact = icon.Prophylaxis.ProtectionFactor;
            double apf = IPEFact * VSHFact * PropFact;
            return apf;
        }

        public static double CalculateDeathDose()
        {
            double LD = getLD(true, false);     //Need of MT and GCSF
            double exp = RadEffChallenge.DoseWholeBody / 60;
            double denominator = -0.2351 * Math.Pow(0.8946, exp) * Math.Pow(exp, -0.2876) + 0.9947;
            return LD / denominator;
        }

        private static double getLD(bool mt, bool gcsf)
        {
            if (mt == false)
                return 4.5;
            else
            {
                if (gcsf == true)
                    return 8.5;
                else
                    return 6.8;
            }

        }
        #endregion

        #region InjuryProfile

        private static void InitInjuryProfile()
        {
            // time in minutes
            List<uint> wbvalues = new List<uint> { 18, 42, 120, 180, 300, 480, 1440, 1800, 2880, 4320, 5760, 11520, 20160, 43200 };
            List<uint> cutvalues = new List<uint> { 6, 60, 480, 600, 1440, 280, 11520};

            for (int i = 0; i < wbvalues.Count(); i++)
                WBInjuryProfile.TimeMinutes[i] = wbvalues[i];

            for (int i = 0; i < cutvalues.Count(); i++)
                CutInjuryProfile.TimeMinutes[i] = cutvalues[i];
        }

        private static void CalculateInjuryProfile()
        {
            int wb = EstimateRangeWBDose();
            int cut = EstimateRangeCutDose();

            List <uint> sevLevelWb = GetWBSeverityLevel(wb);
            List <uint> sevLevelCut = GetCutSeverityLevel(cut);

            for (int i = 0; i < sevLevelWb.Count(); i++)
                WBInjuryProfile.SeverityLevel[i] = sevLevelWb[i];

            for (int i = 0; i < sevLevelCut.Count(); i++)
                CutInjuryProfile.SeverityLevel[i] = sevLevelCut[i];
        }

        private static int EstimateRangeCutDose()
        {
            double CutDose = RadEffChallenge.DoseCoutanous;
            if (CutDose < 2)
            {
                SetCutDescription(0);
                return 0;
            }
            if (CutDose < 15)
            {
                SetCutDescription(1);
                return 1;
            }
            if (CutDose < 40)
            {
                SetCutDescription(2);
                return 2;
            }
            if (CutDose < 550)
            {
                SetCutDescription(3);
                return 3;
            }
            else
            {
                SetCutDescription(4);
                return 4;
            }
        }

        private static int EstimateRangeWBDose()
        {
            double WbDose = RadEffChallenge.DoseWholeBody;
            if (WbDose < 1.25)
            {
                SetWBDescription(0);
                return 0;
            }
            if (WbDose < 3)
            {
                SetWBDescription(1);
                return 1;
            }
            if (WbDose < 4.5)
            {
                SetWBDescription(2);
                return 2;
            }
            if (WbDose < 8.3)
            {
                SetWBDescription(3);
                return 3;
            }
            else
            {
                SetWBDescription(4);
                return 4;
            }
        }

        private static List<uint> GetCutSeverityLevel(int cut)
        {
            switch (cut)
            {
                case 1:
                    return new List<uint>() { 0, 0, 0, 1, 1, 0, 0 };
                case 2:
                    return new List<uint>() { 0, 0, 1, 1, 1, 0, 0 };
                case 3:
                    return new List<uint>() { 0, 1, 1, 1, 1, 2, 3 };
                case 4:
                    return new List<uint>() { 1, 1, 1, 1, 2, 2, 3 };
                default:
                    return new List<uint>() { 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        private static List<uint> GetWBSeverityLevel(int wb)
        {
            switch (wb)
            {
                case 1:
                    return new List<uint>() { 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
                case 2:
                    return new List<uint>() { 0, 0, 2, 2, 3, 2, 1, 0, 0, 0, 0, 2, 2, 3 };
                case 3:
                    return new List<uint>() { 1, 2, 3, 3, 3, 3, 2, 2, 1, 0, 1, 2, 3, 4 };
                case 4:
                    return new List<uint>() { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4 };
                default:
                    return new List<uint>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        private static void CalculateCompInjuryProfile()
        {
            if (RadEffChallenge.DoseWholeBody < 1.25 || RadEffChallenge.DoseCoutanous < 2)
                Composite = false;
            else
            {
                Composite = true;
                int c = 0;
                int i = 0;
                int j = 0;
                while(i < WBInjuryProfile.TimeMinutes.Count() && j < CutInjuryProfile.TimeMinutes.Count())
                {
                    //WBtime < CutTime
                    if(WBInjuryProfile.TimeMinutes[i] < CutInjuryProfile.TimeMinutes[j] )
                    {
                        if (WBInjuryProfile.SeverityLevel[j] > 0)
                        {
                            CompositeInjuryProfile.SeverityLevel[c] = WBInjuryProfile.SeverityLevel[i];
                            CompositeInjuryProfile.TimeMinutes[c] = WBInjuryProfile.TimeMinutes[i];
                            i++;
                            c++;
                        }
                        else
                            i++;
                    }
                    else
                    {
                        //WBTime = CutTime
                        if(WBInjuryProfile.TimeMinutes[i] == CutInjuryProfile.TimeMinutes[j])
                        {
                            uint max = Math.Max(WBInjuryProfile.SeverityLevel[i], CutInjuryProfile.SeverityLevel[j]);
                            if (max > 0)
                            {
                                if (WBInjuryProfile.SeverityLevel[i] == max)
                                    i++;
                                else
                                    j++;
                            }
                            else
                            {
                                i++;
                                j++;
                            }
                        }

                        //WBTime > CutTime
                        else
                        {
                            if (CutInjuryProfile.SeverityLevel[j] > 0)
                            {
                                CompositeInjuryProfile.SeverityLevel[c] = CutInjuryProfile.SeverityLevel[j];
                                CompositeInjuryProfile.TimeMinutes[c] = CutInjuryProfile.TimeMinutes[j];
                                j++;
                                c++;
                            }
                            else
                                j++;
                        }

                        
                    }
                }
            }
        }

        private static void SetWBDescription(int dose)
        {
            switch (dose)
            {
                case 1:
                    RadEffChallenge.Description = "A slight decrease in white blood cell and platelet " +
                                                "count with possible beginningsymptoms of bone marrow damage;" +
                                                " survival is > 90 % unless there are other injuries.";
                    return;
                case 2:
                    RadEffChallenge.Description = "Moderate to severe bone marrow damage occurs; lethality ranges from LD5/60 to LD50/60; " +
                                                "these patients require greater than 30 days recovery, but other injuries would increase the injury severity and likelihood of death.";
                    return;
                case 3:
                    RadEffChallenge.Description = "Severe bone marrow damage occurs; lethality ranges from LD50/60 to LD99/60;" +
                                                " deathoccurs within 3.5 to 6 weeks with the radiation injury alone but is accelerated with other injuries;" +
                                                " with other injuries death may occur within 2 weeks";
                    return;
                case 4:
                    RadEffChallenge.Description = "Bone marrow pancytopenia and moderate intestinal damage occur including diarrhea;" +
                                                " death is expected within 2 to 3 weeks; with other injuries death may occurwithin 2 weeks;" +
                                                " at higher doses, combined gastrointestinal and bone marrowdamage occur with hypotension" +
                                                " and death is expected within 1 to 2.5 weeks or ifother injuries are also present, within 6 days";
                    return;
                default:
                    RadEffChallenge.Description = "No observable injury.";
                    return;
            }
        }

        private static void SetCutDescription(int dose)
        {
            switch (dose)
            {
                case 1:
                    RadEffChallenge.Description = "12 hours to 5 weeks post exposure: erythema, slight edema," +
                                                " possible increased pigmentation; 6 to 7 weeks post exposure: dry desquamation.";
                    return;
                case 2:
                    RadEffChallenge.Description = "Immediate itching; 1 to 3 weeks post exposure: erythema, edema;" +
                                                " 5 to 6weeks post exposure: subcutaneous tissue edema, blisters, moistdesquamation; late effects(> 10 weeks).";
                    return;
                case 3:
                    RadEffChallenge.Description = "Immediate pain, tingling for 1 to 2 days; 1 to 2 weeks post exposure:" +
                                                " erythema, blisters, edema, pigmentation, erosions, ulceration, severe pain;" +
                                                " severe late effects(> 10 weeks).";
                    return;
                case 4:
                    RadEffChallenge.Description = "Immediate pain, tingling, swelling; 1 to 4 days post exposure:" +
                                                " blisters, early ischemia, substantial pain; tissue necrosis within 2 weeks, substantial pain.";
                    return;
                default:
                    RadEffChallenge.Description = "No observable injury.";
                    return;
            }
        }

        #endregion

        #region Icon State
        private static List<string> GetStateCut(int range)
        {
            List<string> result = new List<string>();
            if (range == 1)
                return new List<string> { "WIA", "RTD", "3", "100" };
            if (range == 2)
                return new List<string> { "WIA", "RTD", "3", "100" };
            if (range == 3)
                return new List<string> { "WIA", "CONV", "3", "100" };
            if (range == 4)
                return new List<string> { "WIA", "CONV", "3", "100" };
            else
                return new List<string> { "None" };
        }

        private static List<string> GetStateWB(int range)
        {
            List<string> result = new List<string>();
            if (range == 1)
                return new List<string> { "WIA", "CONV", "2", "100" };
            else
                return new List<string> { "WIA", "CONV", "30", "100" };
        }

        private static void InterpretState(List<string> wbstate, List<string> cutstate, Icon icon)
        {
            bool wb = true, cut = true;
            if (wbstate.Contains("None"))
                wb = false;
            if (cutstate.Contains("None"))
                cut = false;
            if (cut == false && wb == false)
                return;

            RadChallengeProperties.newWIA[0] += Convert.ToInt32(icon.Personnel);

            if (wb == false)
            {
                if (cutstate.Contains("CONV"))
                {
                    RadChallengeProperties.newCONV[2] += Convert.ToInt32(icon.Personnel);
                    return;
                }
                else
                {
                    RadChallengeProperties.newRTD[2] += Convert.ToInt32(icon.Personnel);
                    return;
                }
            }

            if (wbstate[2].Equals("2"))
            {
                RadChallengeProperties.newCONV[1] += Convert.ToInt32(icon.Personnel);
                if (cutstate.Contains("RTD"))
                {
                    RadChallengeProperties.newCONV[ConvertToTable(Convert.ToInt32(wbstate[2]))] = Convert.ToInt32(icon.Personnel);
                    return;
                }
                else
                {
                    int day = Math.Max(Convert.ToInt32(cutstate[2]), Convert.ToInt32(wbstate[2]));
                    RadChallengeProperties.newCONV[ConvertToTable(day)] = Convert.ToInt32(icon.Personnel);
                    return;
                }
            }
            else
            {
                double TimeToDeath = 0;
                double deathdose = CalculateDeathDose();
                if (RadEffChallenge.DoseWholeBody > deathdose)
                {
                    if (RadEffChallenge.DoseWholeBody < 100)
                        TimeToDeath = 429 * Math.Pow(RadEffChallenge.DoseWholeBody, -1.3);
                    else
                        TimeToDeath = 2;
                    RadChallengeProperties.newDOW[ConvertToTable(Convert.ToInt32(TimeToDeath))] = Convert.ToInt32(icon.Personnel);
                    return;
                }
                else
                {
                    RadChallengeProperties.newCONV[8] = Convert.ToInt32(icon.Personnel);
                    return;
                }

            }

    }

        private static void CompletTable()
        {
            for(int i = 1; i < RadChallengeProperties.newCONV.Count(); i++)
            {
                OutputTable.Rows[0].SetField(i, Convert.ToInt32(OutputTable.Rows[0].ItemArray[i]) + RadChallengeProperties.newKIA[i - 1]);
                OutputTable.Rows[1].SetField(i, Convert.ToInt32(OutputTable.Rows[1].ItemArray[i]) + RadChallengeProperties.newDOW[i - 1]);
                OutputTable.Rows[2].SetField(i, Convert.ToInt32(OutputTable.Rows[2].ItemArray[i]) + RadChallengeProperties.newKIA[i - 1] + RadChallengeProperties.newDOW[i - 1]);
                OutputTable.Rows[3].SetField(i, Convert.ToInt32(OutputTable.Rows[3].ItemArray[i]) + RadChallengeProperties.newWIA[i - 1]);
                OutputTable.Rows[4].SetField(i, Convert.ToInt32(OutputTable.Rows[4].ItemArray[i]) + RadChallengeProperties.newCONV[i - 1]);
                OutputTable.Rows[5].SetField(i, Convert.ToInt32(OutputTable.Rows[5].ItemArray[i]) + RadChallengeProperties.newRTD[i - 1]);
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
        #endregion

        #region Output
        private static void CreateTables()
        {
            List<string> tableDetails = new List<string>
                { "New KIA (N)", "New DOW (CRN)", "Sum of New Fatalities", "New WIA (Nuclear)", "New CONV (Nuclear)", "New RTD"};

            OutputTable = InitTable(tableDetails);

        }

        public static DataTable InitTable(List<string> details)
        {
            DataTable table = new DataTable("Report");

            #region ColumnDescription

            DataColumn CasualtyDesc = new DataColumn("CasualtyDescription");
            CasualtyDesc.DataType = typeof(string);
            CasualtyDesc.MaxLength = 30;
            CasualtyDesc.Unique = true;
            CasualtyDesc.AllowDBNull = false;
            CasualtyDesc.Caption = "Description";

            table.Columns.Add(CasualtyDesc);
            #endregion

            #region Columns

            List<string> colName = new List<string>
                { "Day 1", "Day 2", "Day 3", "Day 4", "Day 5", "Day 6", "Day 7", "Days 8-14", "Days 15-30", "Days 31+" };

            foreach (var item in colName)
                AddColumn(table, item);

            #endregion

            #region Rows

            foreach (var item in details)
                AddRow(table, item);

            #endregion

            return table;
        }

        public static void AddColumn(DataTable table, string details)
        {
            DataColumn column = new DataColumn(details);
            column.DataType = typeof(uint);
            column.Unique = false;
            column.AllowDBNull = false;
            column.Caption = details;
            table.Columns.Add(column);
        }

        public static void AddRow(DataTable table, string colName)
        {
            DataRow row = table.NewRow();
            row[0] = colName;
            for (int i = 1; i < 11; i++)
                row[i] = 0;
            table.Rows.Add(row);
        }

        #endregion

        #endregion

    }
}

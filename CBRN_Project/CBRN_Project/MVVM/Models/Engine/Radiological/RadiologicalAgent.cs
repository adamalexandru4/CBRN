using CBRN_Project.MVVM.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Engine.Radiological
{
    public class RadiologicalAgent
    {
        #region Properties

            //Class RadiologicalProperties contain required data to calculate challenge
            private static RadiologicalProperties RadChallengeProperties    { get; set; }

            //Class derivated from EffChallenge 
            private static RadiologicalChallenge RadEffChallenge            { get; set; }
            
            //Injury Profile
            private static InjuryProfile WBInjuryProfile                    { get; set; }
            private static InjuryProfile CutInjuryProfile                   { get; set; }
          
        #endregion


        #region Methods
       

        public static void CalculateChallenge (Icon icon, string izotop)
        {
            //verify type of Challenge and if it is necessary  CalculateEffChallenge
            InitChallengeType(icon, izotop);
            
            Calculate(icon);
            
            GenerateOutput(icon);

        }

        private static void InitChallengeType (Icon icon, string izotop)
        {
            //Verify if exists in challenges radiological agents
            var results = icon.Challenges
                                .Where(c => c.Agent == "Radiological");
            
            //if exist convert to eff chalanges and complete the challenge type
            if (results != null)
            {
                var RDDResult = results.Where(c => c.ChallengeType.Contains("RDD"));
                foreach (var item in RDDResult)
                { 
                    ConvertToEffChallengeRDD(icon, item);
                }
                var FalloutResult = results.Where(c => c.ChallengeType.Contains("Fallout"));
                foreach (var item in RDDResult)
                {
                    ConvertToEffChallengeFallout(icon, item);
                }
            }
        }

        private static void Calculate(Icon icon)
        {
            double TimeToDeath;
            CalculateDeathDose();
            if (RadEffChallenge.DoseWholeBody > RadEffChallenge.DeathDose)
            {
                if (RadEffChallenge.DoseWholeBody < 100)
                    TimeToDeath = 429 * Math.Pow(RadEffChallenge.DoseWholeBody, -1.3);
                else
                {
                    TimeToDeath = 24 * 60;
                }
            }
        }

        private static void GenerateOutput(Icon icon)
        {

        }

        #endregion



        #region Auxiliar methods
        public static void ConvertToEffChallengeRDD(Icon icon, Challenge item)
        {
            RadChallengeProperties.TotalTime += item.TimeValues.Last();
            double apf = getAPF(icon);
            double Zfactor = GetRDDFactor("cloudshine");
            RadChallengeProperties.CutaneousCloudShine += ComputeChallenge(item, apf, Zfactor);
            RadChallengeProperties.WholeBodyCloudShine += ComputeChallenge(item, apf, Zfactor);

            Zfactor = GetRDDFactor("groundshine");
            RadChallengeProperties.WholeBodyGroundshine += ComputeChallenge(item, apf, Zfactor);
            RadChallengeProperties.CutaneousGroundShine += ComputeChallenge(item, apf, Zfactor);

            Zfactor = GetRDDFactor("skin");
            RadChallengeProperties.CutaneousSkin += ComputeChallenge(item, apf, Zfactor);
           
            Zfactor = GetRDDFactor("inhalation");
            RadChallengeProperties.WholeBodyInhalation += ComputeChallenge(item, apf, Zfactor);

            RadEffChallenge.DoseCoutanous += RadChallengeProperties.GetCutaneousDose();
            RadEffChallenge.DoseWholeBody += RadChallengeProperties.GetWholeBodyDose();
        }

        public static void ConvertToEffChallengeFallout(Icon icon, Challenge item)
        {
            RadChallengeProperties.TotalTime += item.TimeValues.Last();
            double apf = getAPF(icon);
            double Zfactor = GetFalloutFactor("groundshine");
            RadChallengeProperties.WholeBodyGroundshineFallout += ComputeChallenge(item, apf, Zfactor);
            RadChallengeProperties.CutaneousSkinFallout += ComputeChallenge(item, apf, Zfactor);
            Zfactor = GetFalloutFactor("skin beta");
            RadChallengeProperties.CutaneousSkinFallout += ComputeChallenge(item, apf, Zfactor);
            Zfactor = GetFalloutFactor("skin");
            RadChallengeProperties.CutaneousSkinFallout += ComputeChallenge(item, apf, Zfactor);

            RadEffChallenge.DoseCoutanous += RadChallengeProperties.CutaneousSkinFallout;
            RadEffChallenge.DoseWholeBody += RadChallengeProperties.WholeBodyGroundshineFallout;
        }

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

        private static double GetRDDFactor(string context)
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

        private static double GetFalloutFactor(string context)
        {
            switch (context)
            {
                case "groundshine":
                    return 1;
                case "skin beta":
                    return 1;
                case "skin":
                    return 1;
                default:
                    return 1; 
            }
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

        public static void CalculateDeathDose()
        {
            double LD = getLD();
            double exp = RadEffChallenge.DoseWholeBody / RadChallengeProperties.TotalTime;
            double denominator = -0.2351 * Math.Pow(0.8946,exp) * Math.Pow(exp,-0.2876) + 0.9947;
            RadEffChallenge.DeathDose = LD / denominator;
        }

        private static double getLD()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

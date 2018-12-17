using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    public enum ActivityLevel { AtRest, Light, Moderate, Heavy }

    public class BreathingRate
    {
        #region Properties

        public float ChemAg_Ih { get; set; }
        public float BioAg_RadPar_Ih { get; set; }
        public float ChemAg_UnitlessFactor { get; set; }

        private ActivityLevel breathingRateActivityLevel { get; set; } = ActivityLevel.Light;
        public string BreathingRateActivityLevel
        {
            get
            {
                switch(breathingRateActivityLevel)
                {
                    case ActivityLevel.AtRest:
                        return "At rest";
                    case ActivityLevel.Light:
                        return "Light";
                    case ActivityLevel.Moderate:
                        return "Moderate";
                    case ActivityLevel.Heavy:
                        return "Heavy";
                    default:
                        return "None";
                }
            }
            set
            {
                switch(value)
                {
                    case "At rest":
                        breathingRateActivityLevel = ActivityLevel.AtRest;
                        break;
                    case "Light":
                        breathingRateActivityLevel = ActivityLevel.Light;
                        break;
                    case "Moderate":
                        breathingRateActivityLevel = ActivityLevel.Moderate;
                        break;
                    case "Heavy":
                        breathingRateActivityLevel = ActivityLevel.Heavy;
                        break;
                    default:
                        breathingRateActivityLevel = ActivityLevel.Light;
                        break;
                }
            }
        }
        
        #endregion

        #region Constructors

        public BreathingRate()
        {
            this.ChemAg_Ih = 0.015F;
            this.BioAg_RadPar_Ih = 0.015F;
            this.ChemAg_UnitlessFactor = 1F;
            this.breathingRateActivityLevel = ActivityLevel.Light;
        }

        public BreathingRate(ActivityLevel activityLevel)
        {
            switch (activityLevel)
            {
                case ActivityLevel.AtRest:
                    {
                        this.ChemAg_Ih = 0.0075F;
                        this.BioAg_RadPar_Ih = 0.0075F;
                        this.ChemAg_UnitlessFactor = 0.5F;
                        this.breathingRateActivityLevel = ActivityLevel.AtRest;
                    } break;
                case ActivityLevel.Light:
                    {
                        this.ChemAg_Ih = 0.015F;
                        this.BioAg_RadPar_Ih = 0.015F;
                        this.ChemAg_UnitlessFactor = 1F;
                        this.breathingRateActivityLevel = ActivityLevel.Light;
                    } break;
                case ActivityLevel.Moderate:
                    {
                        this.ChemAg_Ih = 0.03F;
                        this.BioAg_RadPar_Ih = 0.03F;
                        this.ChemAg_UnitlessFactor = 2F;
                        this.breathingRateActivityLevel = ActivityLevel.Moderate;
                    } break;
                case ActivityLevel.Heavy:
                    {
                        this.ChemAg_Ih = 0.075F;
                        this.BioAg_RadPar_Ih = 0.075F;
                        this.ChemAg_UnitlessFactor = 5F;
                        this.breathingRateActivityLevel = ActivityLevel.Heavy;
                    } break;
            }
        }

        public BreathingRate(float ChemAg_Ih, float BioAg_RadPar_Ih)
        {
            this.ChemAg_Ih = ChemAg_Ih;
            this.BioAg_RadPar_Ih = BioAg_RadPar_Ih;
            this.ChemAg_UnitlessFactor = this.ChemAg_Ih / 0.015F;
        }

        #endregion

        #region Methods for MVVM

        public static List<string> GetActivityLevelList()
        {
            List<string> list = new List<string>
            {
                "At rest",
                "Light",
                "Moderate",
                "Heavy"
            };

            return list;
        }

        #endregion
    }
}

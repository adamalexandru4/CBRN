using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Engine.Radiological
{
    class RadiologicalProperties
    {

        #region Properties
        public enum TypeOfRadChallenge { None, RDD, Fallout};

        // Type of challenge RDD or Fallout
        private TypeOfRadChallenge challengeType        { get; set; } = TypeOfRadChallenge.None;
        public string ChallengeType                     
        {
            get
            {
                switch(challengeType)
                {
                    case TypeOfRadChallenge.RDD:
                        return "RDD";

                    case TypeOfRadChallenge.Fallout:
                        return "Fallout";

                    default:
                        return "None";
                }
            }
            set
            {
                switch (value)
                {
                    case "RDD":
                        challengeType = TypeOfRadChallenge.RDD;
                        break;
                    case "Fallout":
                        challengeType = TypeOfRadChallenge.Fallout;
                        break;
                }
            }
        }

        //RDD Properties
        // Whole-Body factors 
        public double WholeBodyInhalation               { get; set; }
        public double WholeBodyCloudShine               { get; set; }
        public double WholeBodyGroundshine              { get; set; }

        // Coutaneous factors
        public double CutaneousSkin                     { get; set; }
        public double CutaneousCloudShine               { get; set; }
        public double CutaneousGroundShine              { get; set; }
        //Fallout Properties
        public double WholeBodyGroundshineFallout       { get; set; }
        public double CutaneousSkinFallout              { get; set; }

        #endregion

        #region Methods
        public double GetWholeBodyDose()
        {
            return WholeBodyCloudShine + WholeBodyCloudShine + WholeBodyInhalation;
        }

        public double GetCutaneousDose()
        {
            return CutaneousCloudShine + CutaneousGroundShine + CutaneousSkin;
        }
        #endregion

    }
}

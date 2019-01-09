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
        #region RDD Properties
        //RDD Properties
        public List<string> Izotop                             { get; set; }
        // Whole-Body factors 
        public double WholeBodyInhalation               { get; set; }
        public double WholeBodyCloudShine               { get; set; }
        public double WholeBodyGroundshine              { get; set; }

        // Coutaneous factors
        public double CutaneousSkin                     { get; set; }
        public double CutaneousCloudShine               { get; set; }
        public double CutaneousGroundShine              { get; set; }
        #endregion

        #region Fallout Properties
        //Fallout Properties
        public double WholeBodyGroundshineFallout       { get; set; }
        public double CutaneousSkinFallout              { get; set; }

        //State for Icons
        public List<int> newDOW;
        public List<int> newWIA;
        public List<int> newCONV;
        public List<int> newRTD;
        public List<int> newKIA;

        #endregion

        #endregion

        #region Methods
        public RadiologicalProperties()
        {
            SetLists();
        }

        private void SetLists()
        {
            newDOW = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newWIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newCONV = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newRTD = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            newKIA = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        public double GetWholeBodyDose()
        {
            return WholeBodyCloudShine + WholeBodyCloudShine + WholeBodyInhalation;
        }

        public double GetCutaneousDose()
        {
            return CutaneousCloudShine + CutaneousGroundShine + CutaneousSkin;
        }

        public void ResetValue()
        {
            this.challengeType = TypeOfRadChallenge.None;
            this.CutaneousCloudShine = 0;
            this.CutaneousGroundShine = 0;
            this.CutaneousSkin = 0;
            this.CutaneousSkinFallout = 0;
            this.Izotop = null;
            this.WholeBodyCloudShine = 0;
            this.WholeBodyGroundshine = 0;
            this.WholeBodyGroundshineFallout = 0;
            this.WholeBodyInhalation = 0;
        }
        #endregion

    }
}

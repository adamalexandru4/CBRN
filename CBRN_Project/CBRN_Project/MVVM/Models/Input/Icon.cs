using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    public class Icon
    {
        #region Properties

        public string Name                                      { get; set; }
        public int ID                                           { get; set; }
        public double Personnel                                 { get; set; }
        
        public BreathingRate BreathingRate                      { get; set; }
        public double BodySurfaceArea                           { get; set; } = 0.9;
        public ProtFactors IPE                                  { get; set; }
        public VehicleShelter Vehicle_Shelter                   { get; set; }
        public Prophylaxis Prophylaxis                          { get; set; }
        public Uniform Uniform                                  { get; set; }

        public List<Challenge> Challenges                       { get; set; }
        public List<EffChallenge> EffChallenges                 { get; set; }

        #endregion

        #region Constructors

        public Icon(int iconId)
        {
            Vehicle_Shelter = new VehicleShelter();
            Prophylaxis = new Prophylaxis();
            Uniform = new Uniform();

            ID = iconId;
            Name = "Icon " + ID.ToString();
        }

        #endregion

        #region Methods

        public static Icon CreateNewIcon(int iconId)
        {
            return new Icon(iconId);
        }

        #endregion
    }
}

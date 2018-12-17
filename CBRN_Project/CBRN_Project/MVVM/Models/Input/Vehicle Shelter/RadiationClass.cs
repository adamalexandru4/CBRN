using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models
{
    public enum RadiationClass { ArmoredPersoneel, EarthShelter, ExposedDismounted,
                                 FosholeNuclear, MasonryBuilding, MultiStoryBrickBuilding,
                                 Tank, Tent, Truck, Van, WoodFrameBuilding, Default};

    public class RadiationShieldingProtection
    {
        private RadiationClass radiationClass;
        
        public float NeutronRadiation { get; set; }
        public float GammaRadiation { get; set; }
        public float BetaRadiation { get; set; } = float.PositiveInfinity;

        public string RadiationClassValue
        {
            get
            {
                switch(radiationClass)
                {
                    case RadiationClass.ArmoredPersoneel:
                        return "Armored Personeel";
                    case RadiationClass.EarthShelter:
                        return "Earth Shelter";
                    case RadiationClass.ExposedDismounted:
                        return "Exposed/Dismounted";
                    case RadiationClass.FosholeNuclear:
                        return "Foshole (nuclear only)";
                    case RadiationClass.MasonryBuilding:
                        return "Masonry Building";
                    case RadiationClass.MultiStoryBrickBuilding:
                        return "Multi-Story Brick Building";
                    case RadiationClass.Tank:
                        return "Tank";
                    case RadiationClass.Tent:
                        return "Tent";
                    case RadiationClass.Truck:
                        return "Truck";
                    case RadiationClass.Van:
                        return "Van";
                    case RadiationClass.WoodFrameBuilding:
                        return "Wood Frame Building";
                    default:
                        return "Default";
                }
            }
            set
            {
                switch (value)
                {
                    case "Armored Personeel":
                        radiationClass = RadiationClass.ArmoredPersoneel;
                        NeutronRadiation = 1.22F;
                        GammaRadiation = 2.70F;
                        break;
                    case "Earth Shelter":
                        radiationClass = RadiationClass.EarthShelter;
                        NeutronRadiation = 16.67F;
                        GammaRadiation = 66.67F;
                        break;
                    case "Exposed/Dismounted":
                        radiationClass = RadiationClass.ExposedDismounted;
                        NeutronRadiation = 1;
                        GammaRadiation = 1;
                        break;
                    case "Foshole (nuclear only)":
                        radiationClass = RadiationClass.FosholeNuclear;
                        NeutronRadiation = 3;
                        GammaRadiation = 10;
                        break;
                    case "Masonry Building":
                        radiationClass = RadiationClass.MasonryBuilding;
                        NeutronRadiation = 8.33F;
                        GammaRadiation = 6.67F;
                        break;
                    case "Multi-Story Brick Building":
                        radiationClass = RadiationClass.MultiStoryBrickBuilding;
                        NeutronRadiation = 1.33F;
                        GammaRadiation = 1.56F;
                        break;
                    case "Tank":
                        radiationClass = RadiationClass.Tank;
                        NeutronRadiation = 3.57F;
                        GammaRadiation = 10;
                        break;
                    case "Tent":
                        radiationClass = RadiationClass.Tent;
                        NeutronRadiation = 1;
                        GammaRadiation = 1;
                        break;
                    case "Truck":
                        radiationClass = RadiationClass.Truck;
                        NeutronRadiation = 1;
                        GammaRadiation = 1.25F;
                        break;
                    case "Van":
                        radiationClass = RadiationClass.Van;
                        NeutronRadiation = 1.05F;
                        GammaRadiation = 1.05F;
                        break;
                    case "Wood Frame Building":
                        radiationClass = RadiationClass.WoodFrameBuilding;
                        NeutronRadiation = 1.39F;
                        GammaRadiation = 1.22F;
                        break;
                    default:
                        radiationClass = RadiationClass.Default;
                        break;
                }
            }
        }

        #region Constructor

        public RadiationShieldingProtection()
        {

        }

        #endregion
    }
}

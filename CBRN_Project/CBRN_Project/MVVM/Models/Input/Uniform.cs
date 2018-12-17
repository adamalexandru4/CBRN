using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models
{
    public class Uniform
    {
        private float thresoldThermalFluence { get; set; }

        private string uniformType { get; set; }
        public string UniformType
        {
            get
            {
                return uniformType;
            }

            set
            {
                uniformType = value;

                switch(value)
                {
                    case "Bare Skin":
                        thresoldThermalFluence = 109;
                        break;
                    case "BDU + T-shirt":
                        thresoldThermalFluence = 310;
                        break;
                    case "BDU + T-shirt + Airspace":
                        thresoldThermalFluence = 630;
                        break;
                    case "BDO":
                        thresoldThermalFluence = 420;
                        break;
                    case "BDO + Airspace":
                        thresoldThermalFluence = 670;
                        break;
                    case "BDO + BDU + T-shirt":
                        thresoldThermalFluence = 1300;
                        break;
                    case "BDO + BDU + T-shirt + Airspace":
                        thresoldThermalFluence = 2010;
                        break;
                    default:
                        break;
                }
            }
        }

        public Uniform()
        {

        }
    }
}

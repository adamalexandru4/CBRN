using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models
{
    public enum ColProType { WithColPro, WithoutColPro };

    public class InhalationPercutaneousVapourProtection
    {
        public float Inhalation { get; set; }
        public float PercVap    { get; set; }
        public float PercLiq    { get; set; } = float.PositiveInfinity;

        public InhalationPercutaneousVapourProtection()
        {
            
        }

        public void SetWithColPro(string ventilationClass)
        {
            switch(ventilationClass)
            {
                case "Vehicle w/ColPro":
                    Inhalation = 3000;
                    PercVap = 3000;
                    break;
                case "Shelter w/ColPro":
                    Inhalation = 3000;
                    PercVap = 3000;
                    break;
                default:
                    break;
            }
        }

        public void SetWithoutColPro(float _AER, float _Duration, float _Occupancy)
        {
            double AER = (double)_AER;
            double Duration = (double)_Duration;
            double Occupancy = (double)_Occupancy;

            double val = ((AER * Duration) / ((AER * Duration) + (Math.Pow(Math.E, AER * (-1) * Occupancy)) - (Math.Pow(Math.E, AER * (Duration - Occupancy)))));

            Inhalation = (float)val;
            PercVap = (float)val;
        }

        public void SetDefault()
        {
            Inhalation = PercVap = PercLiq = 1;
        }
    }
}

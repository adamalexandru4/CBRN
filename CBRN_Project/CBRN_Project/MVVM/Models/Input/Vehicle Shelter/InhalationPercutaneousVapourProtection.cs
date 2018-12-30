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
        public double Inhalation { get; set; }
        public double PercVap    { get; set; }
        public double PercLiq    { get; set; } = double.PositiveInfinity;

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

        public void SetWithoutColPro(double _AER, double _Duration, double _Occupancy)
        {
            double AER = _AER;
            double Duration = _Duration;
            double Occupancy = _Occupancy;

            double val = ((AER * Duration) / ((AER * Duration) + (Math.Pow(Math.E, AER * (-1) * Occupancy)) - (Math.Pow(Math.E, AER * (Duration - Occupancy)))));

            Inhalation = val;
            PercVap = val;
        }

        public void SetDefault()
        {
            Inhalation = PercVap = PercLiq = 1;
        }
    }
}

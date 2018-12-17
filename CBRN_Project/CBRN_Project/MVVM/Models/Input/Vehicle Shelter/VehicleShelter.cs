using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models
{
    public class VehicleShelter
    {
        /* Vehicles and shelters are assumed to completely protect icons from liquid chemical agent challenges */

        public InhalationPercutaneousVapourProtection   InhalationPercutaneousVapourProtection  { get; set; }
        public RadiationShieldingProtection             RadiationProtection                     { get; set; }
        public BlastShieldingProtection                 BlastProtection                         { get; set; }

        /* Thermal fluence is not dependent on each icon */

        public VehicleShelter()
        {
            InhalationPercutaneousVapourProtection = new InhalationPercutaneousVapourProtection();
            RadiationProtection = new RadiationShieldingProtection();
            BlastProtection = new BlastShieldingProtection();
        }
    }
}

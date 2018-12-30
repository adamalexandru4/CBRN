using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    class MethParams
    {
        public string Agent { get; set; }
        public List<string> ChTypes { get; set; }

        public double   T_MTF   { get; set; } = 30;
        public double   T_death { get; set; } = 15;
        public bool     FlagMT  { get; set; } = true;
        public uint     CasCrit { get; set; } = 1;
        public uint     D_trt   { get; set; } = 1;
    }
}

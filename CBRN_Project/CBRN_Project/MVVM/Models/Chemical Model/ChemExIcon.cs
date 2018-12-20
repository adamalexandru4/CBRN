using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models.Chemical
{
    class ChemExIcon
    {
        public Icon                         Icon    { get; set; }
        public Dictionary<string, double>   Pbties  { get; set; }
        public Dictionary<string, double>   Pops    { get; set; }
    }
}

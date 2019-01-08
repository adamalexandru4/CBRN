using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Engine.Radiological
{
    class RadiologicalChallenge : EffChallenge
    {
        public double DoseWholeBody     { get; set; }
        public double DoseCoutanous     { get; set; }
        public List<string> Izotop      { get; set; }
        public string Description       { get; set; }



        public RadiologicalChallenge()
         : base ("None","None",0.0) { }
    }
}

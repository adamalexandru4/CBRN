using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRN_Project.MVVM.Models.Input
{
    public class InjuryProfile
    {

        public int IdIcon { get; set; }
        private readonly List<uint> _timeMinutes = new List<uint>();
        private readonly List<uint> _severityLevel = new List<uint>();

        public List<uint> TimeMinutes { get { return _timeMinutes; } }
        public List<uint> SeverityLevel { get { return _severityLevel; } }

        public void AddTimeMinutes(int minutes)
        {
            if (minutes > 0)
                _timeMinutes.Add(Convert.ToUInt32(minutes));
        }

        public void AddSeverityLevel(int severityLevel)
        {
            if (severityLevel >= 0 && severityLevel <= 4)
                _severityLevel.Add(Convert.ToUInt32(severityLevel));
        }
    
    }
}

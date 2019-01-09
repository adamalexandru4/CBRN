using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    public class Challenge
    {
        public string Agent         { get; set; }
        public string ChallengeType { get; set; }
        public double Step          { get; set; }
        public List<double> Values  { get; set; }
        public List<int> TimeValues { get; set; }
    }
}

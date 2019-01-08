using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    public class EffChallenge
    {
        public string Agent                 { get; set; }
        public string ChallengeType         { get; set; }
        public double Value                 { get; set; }

        public EffChallenge(string agent, string challengeType, double value)
        {
            Agent = agent;
            ChallengeType = challengeType;
            Value = value;
        }

    }
}

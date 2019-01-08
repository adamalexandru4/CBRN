namespace CBRN_Project.MVVM.Models
{
    public class EffChallenge
    {
        public string Agent         { get; set; }
        public string ChallengeType { get; set; }
        public double Value         { get; set; }
        public double SecondValue   { get; set; }


        public EffChallenge (string agent, string challengeType, double value, double secvalue)
        {
            this.Agent = agent;
            this.ChallengeType = challengeType;
            this.Value = value;
            this.SecondValue = secvalue;
        }
    }
}

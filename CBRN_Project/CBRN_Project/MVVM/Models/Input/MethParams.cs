using System.Collections.Generic;
using System.Linq;

namespace CBRN_Project.MVVM.Models
{
    public enum ChemAgents { GA, GB, GD, GF, VX, HD, CG, CL2, NH3, AC, CK, H2S }
    public enum RadAgents { RDD, Fallout }
    public enum NuclearAgents { IWBR, Blast, ThermFluence }
    public enum BioAgents { Anthrax, Brucellosis, Glanders, Melioidosis }

    public enum ChallengeTypes { Inhaled, PercLiquid, PercVapour}

    public class MethParams
    {
        public string       Agent     { get; set; }
        public List<string> ChTypes   { get; set; }
        public bool         CalcEffCh { get; set; }

        public double   T_MTF   { get; set; } = 30;
        public double   T_death { get; set; } = 15;
        public bool     FlagMT  { get; set; } = true;
        public uint     CasCrit { get; set; } = 1;
        public uint     D_trt   { get; set; } = 1;

        public MethParams()
        {
            ChTypes = new List<string>();
        }

        public MethParams(string agent, List<string> chTypes, bool calcEffCh)
        {
            Agent       = agent;
            ChTypes     = chTypes;
            CalcEffCh   = calcEffCh;
        }

        public static List<string> GetChemAgents()
        {
            return System.Enum.GetNames(typeof(ChemAgents)).ToList();
        }

        public static List<string> GetChallengeTypesForChemAgents(ChemAgents chemAgent)
        {
            List<string> list = new List<string>();

            switch(chemAgent)
            {
                case ChemAgents.VX:
                    list.AddRange(new string[]{
                        "Inhaled",
                        "Percutaneous Liquid" });
                    break;
                default:
                    break;
            }

            return list;
        }
    }
}

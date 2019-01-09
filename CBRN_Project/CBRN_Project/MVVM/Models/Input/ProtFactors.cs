using System.Collections.Generic;

namespace CBRN_Project.MVVM.Models
{
    public enum IPEClass { None, Mask, SuitBoots, SuitBootsMask, Full, Default }

    public class ProtFactors
    {
        #region Properties

        private IPEClass ipeClass { get; set; } = IPEClass.None;
        public string IpeClass
        {
            get
            {
                switch(ipeClass)
                {
                    case IPEClass.Mask:
                        return "Mask";
                    case IPEClass.SuitBoots:
                        return "Suit and boots";
                    case IPEClass.SuitBootsMask:
                        return "Suit, boots and mask";
                    case IPEClass.Full:
                        return "Full protection";
                    case IPEClass.Default:
                        return "Default";
                    default:
                        return "None";
                }
            }
            set
            {
                switch (value)
                {
                    case "Mask":
                        ipeClass = IPEClass.Mask;
                        SetMaskValues();
                        break;
                    case "Suit and boots":
                        ipeClass = IPEClass.SuitBoots;
                        SetSuitBootsValues();
                        break;
                    case "Suit, boots and mask":
                        ipeClass = IPEClass.SuitBootsMask;
                        SetSuitBootsMaskValues();
                        break;
                    case "Full protection":
                        ipeClass = IPEClass.Full;
                        SetFullValues();
                        break;
                    case "Default":
                        ipeClass = IPEClass.Default;
                        break;
                    default:
                        ipeClass = IPEClass.None;
                        SetNoneValues();
                        break;
                }
            }
        }

        public double Inhalation { get; set; }
        public double PervVap    { get; set; }
        public double PercLiq    { get; set; }
        public double BetaRad    { get; set; }
        public double GammaRad   { get; set; }
        public double NeutronRad { get; set; }
        public double NucBlast   { get; set; }

        #endregion

        #region Constructors

        public ProtFactors()
        {
            this.Inhalation = 1;
            this.PervVap = 1;
            this.PercLiq = 1;
            this.BetaRad = 1;
            this.GammaRad = 1;
            this.NeutronRad = 1;
            this.NucBlast = 1;
        }

        public ProtFactors(double inhalation, double pervVap, double percLiq, double betaRad)
        {
            Inhalation = inhalation;
            PervVap = pervVap;
            PercLiq = percLiq;
            BetaRad = betaRad;
            GammaRad = NeutronRad = NucBlast = 1;
        }

        #endregion

        #region Methods for MVVM

        public static List<string> GetProtectionFactorsList()
        {
            return new List<string>
            {
                "None",
                "Mask",
                "Suit and boots",
                "Suit, boots and mask",
                "Full protection"
            };
        }

        void SetMaskValues()
        {
            Inhalation = 1667;
        }

        void SetSuitBootsValues()
        {
            PervVap = 9;
        }

        void SetSuitBootsMaskValues()
        {
            Inhalation = 1667;
            PercLiq = 13;
            PervVap = 13;
            BetaRad = 13;
        }

        void SetFullValues()
        {
            Inhalation = 1667;
            PervVap = double.PositiveInfinity;
            PercLiq = double.PositiveInfinity;
            BetaRad = double.PositiveInfinity;
        }

        void SetNoneValues()
        {
            Inhalation = PercLiq = PervVap = BetaRad = 1;
        }
        #endregion
    }
}

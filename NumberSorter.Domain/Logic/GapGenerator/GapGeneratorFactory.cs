using NumberSorter.Core.Logic.Algorhythm.GapGenerator;
using NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base;

namespace NumberSorter.Domain.Logic
{
    public static class GapGeneratorFactory
    {
        public static IGapGenerator GetGapGenerator(GapGeneratorType type)
        {
            switch (type)
            {
                case GapGeneratorType.Ciura:
                    return new CiuraGapGenerator();
                case GapGeneratorType.CiuraExtended:
                    return new CiuraExtendedGapGenerator();
                case GapGeneratorType.Knuth:
                    return new KnuthGapGenerator();
                case GapGeneratorType.Tokuda:
                    return new TokudaGapGenerator();
                default:
                    return null;
            }
        }
    }
}
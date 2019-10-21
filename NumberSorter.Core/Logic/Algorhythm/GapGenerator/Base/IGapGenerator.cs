using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.GapGenerator.Base
{
    public interface IGapGenerator
    {
        int[] GenerateGaps(int arraySize);
    }
}

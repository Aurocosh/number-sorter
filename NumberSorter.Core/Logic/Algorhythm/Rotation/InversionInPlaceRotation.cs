using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class InversionInPlaceRotation<T> : ILocalRotationAlgothythm<T>
    {
        public InversionInPlaceRotation()
        {
        }

        public void Rotate(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            var fullRun = new SortRun(leftRun.Start, leftRun.Length + rightRun.Length);

            var newLeftRun = new SortRun(leftRun.Start, rightRun.Length);
            var newRightRun = new SortRun(leftRun.Start + rightRun.Length, leftRun.Length);

            SortRunUtility.InvertRun(list, fullRun);
            SortRunUtility.InvertRun(list, newLeftRun);
            SortRunUtility.InvertRun(list, newRightRun);
        }
    }
}

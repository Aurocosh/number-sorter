using NumberSorter.Domain.Container;
using System;

namespace NumberSorter.Domain.Audiolizers.Base
{
    public interface IStateAudiolizer
    {
        void Init(SortLog<int> sortLog);
        void Play(SortState<int> sortState);
    }
}

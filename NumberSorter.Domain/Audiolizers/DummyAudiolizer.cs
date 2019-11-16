using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;
using System;

namespace NumberSorter.Domain.Audiolizers
{
    public class DummyAudiolizer : IStateAudiolizer
    {
        public void Init(SortLog<int> sortLog)
        {
        }

        public void Play(SortState<int> sortState)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}

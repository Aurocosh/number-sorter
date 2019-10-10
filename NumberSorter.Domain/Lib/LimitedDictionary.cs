using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Lib
{
    public class LimitedDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Queue<TKey> _orderedKeys = new Queue<TKey>();

        public int MaxItemsToHold { get; set; }

        public LimitedDictionary(int maxItemsToHold)
        {
            MaxItemsToHold = maxItemsToHold;
        }

        public new void Add(TKey key, TValue value)
        {
            _orderedKeys.Enqueue(key);
            if (MaxItemsToHold != 0 && Count >= MaxItemsToHold)
                Remove(_orderedKeys.Dequeue());

            base.Add(key, value);
        }

        new public TValue this[TKey key] {
            get => base[key];
            set => Add(key, value);
        }
    }
}

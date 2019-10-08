﻿using NumberSorter.Domain.Container.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Domain.Container.Actions
{
    public class LogRead<T> : LogAction<T> where T : IEquatable<T>
    {
        public int Index { get; }
        public T Value { get; }

        public LogRead(int index, T value)
        {
            Index = index;
            Value = value;
        }

        public override string ToString() => $"Value {Value} read from index {Index}.";
    }
}

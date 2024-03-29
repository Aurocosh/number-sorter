﻿using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace NumberSorter.Domain.Serialization
{
    public sealed class JsonErrorLogger
    {
        public bool HasErrors => _errors.Count > 0;
        public IReadOnlyCollection<string> Errors => _errors;

        private readonly List<string> _errors = new List<string>();

        public void LogError(object sender, ErrorEventArgs args)
        {
            _errors.Add(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }

        public void Clear() => _errors.Clear();
    }
}

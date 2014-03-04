using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE.Library
{
    public sealed class Keyword
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        internal Keyword(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} = {1}", Key, Value);
        }
    }
}
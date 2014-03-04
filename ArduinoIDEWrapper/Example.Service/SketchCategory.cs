using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE.Example
{
    public sealed class SketchCategory
    {
        string name;
        public string Name
        {
            get;
            internal set;
        }
        public SketchCategory Child { get; internal set; }

        public override string ToString()
        {
            StringBuilder stringish = new StringBuilder();
            var cat = this;
            while (cat != null)
            {
                stringish.Append("->");
                stringish.Append(cat.Name);
                cat = cat.Child;
            }
            return stringish.ToString();
        }
    }

        
}

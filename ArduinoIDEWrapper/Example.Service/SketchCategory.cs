using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE.Example.Service
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
    }

        
}

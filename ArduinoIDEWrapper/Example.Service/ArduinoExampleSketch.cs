using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Example
{
    public sealed class ArduinoExampleSketch : ArduinoSketch
    {
        public ArduinoExampleSketch(string fullPathToSketchFile)
            : base(fullPathToSketchFile)
        {
        }


        public override void InitializeCategoryFromFilePath()
        {
            //Usually the parent directory of the INO file would be of the same name as the sketch file, which is just there for separation purpose. So we avoid it.
            var dir = new DirectoryInfo(fullPathToSketchFile).Parent;

            //The depth of the levels used to categorize the example projects seems to be random. So i prferred to write a linked list kind of stuff that will work
            //for any number of levels.
            SketchCategory prev = null, curr = null;

            while (dir.Name != "examples")
            {
                curr = new SketchCategory();
                curr.Name = dir.Name;
                curr.Child = prev;
                prev = curr;

                dir = dir.Parent;
            }
            this.category = prev;
        }
    }
}

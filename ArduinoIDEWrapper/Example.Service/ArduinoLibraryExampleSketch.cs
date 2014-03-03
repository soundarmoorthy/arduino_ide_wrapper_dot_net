using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;

namespace Arduino.IDE.Example.Service
{
    
    internal sealed class ArduinoLibraryExampleSketch : ArduinoSketch
    {
        public ArduinoLibraryExampleSketch(string fullPath)
            : base(fullPath)
        {

        }

        //Remember that the Examples inside the library 
        public override void InitializeCategoryFromFilePath()
        {
            //Usually the parent directory of the INO file would be of the same name as the sketch file, which is just there for separation purpose. So we avoid it.
            var dir = new DirectoryInfo(fullPathToSketchFile).Parent;

            //The depth of the levels used to categorize the example projects seems to be random. So i prferred to write a linked list kind of stuff that will work
            //for any number of levels.
            SketchCategory prev = null;
            SketchCategory curr = new SketchCategory();
            while (dir.Name != "libraries")
            {
                if (dir.Name == "examples")
                    goto Skip;

                curr = new SketchCategory();
                curr.Name = dir.Name;
                curr.Child = prev;
                prev = curr;

            Skip:
                dir = dir.Parent;
            }
            this.category = prev;
        }

        protected override char[] GetContext()
        {
            var dir = new DirectoryInfo(fullPathToSketchFile);
            while (dir.Name != "examples")
                dir = dir.Parent;
            using (StreamReader reader = new StreamReader(Path.Combine(dir.Parent.FullName, "library.properties")))
            {
                var size = reader.BaseStream.Length;
                char[] buffer = new char[size];
                reader.ReadBlock(buffer, 0, (int)size);
                return buffer;
            }
        }
    }
}

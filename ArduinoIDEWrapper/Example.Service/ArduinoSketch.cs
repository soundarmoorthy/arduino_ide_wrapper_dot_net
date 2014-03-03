using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Arduino.IDE.Example.Service
{
    public abstract class ArduinoSketch
    {
        protected string fullPathToSketchFile;
        protected ArduinoSketch(string fullPathToSketchFile)
        {
            if (!File.Exists(fullPathToSketchFile))
                throw new FileNotFoundException("The specified file does not exist");
            this.fullPathToSketchFile = fullPathToSketchFile;
        }

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(fullPathToSketchFile);
            }
        }

        public string Description
        {
            get
            {
                return GetDescription();
            }
        }

        protected virtual string GetDescription()
        {
            using (StreamReader reader = new StreamReader(fullPathToSketchFile))
            {
                char[] buffer = new char[1024]; //Read 1 Kb of information from the file.
                reader.ReadBlock(buffer, 0, 1024);
                return new String(buffer);
            }
        }

        public string FullPath { get { return fullPathToSketchFile; } }

        protected SketchCategory category;
        public SketchCategory Category
        {
            get
            {
                if (category == null)
                    InitializeCategoryFromFilePath();
                return category;
            }
        }

        public abstract void InitializeCategoryFromFilePath();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Arduino.IDE.Example
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

        public char[] Context
        {
            get
            {
                return GetContext();
            }
        }

        protected virtual char[] GetContext()
        {
            using (StreamReader reader = new StreamReader(fullPathToSketchFile))
            {
                char[] buffer = new char[512]; //Read 512 bytes of information from the file.
                reader.ReadBlock(buffer, 0, 512);
                return buffer;
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

        public override string ToString()
        {
            return String.Join(Environment.NewLine, Name, Context, Category, FullPath);
        }
    }
}
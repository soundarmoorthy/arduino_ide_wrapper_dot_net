using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace Arduino.IDE.Wrapper
{
    public sealed class ArduinoExample
    {
        string fullPath;
        public ArduinoExample(string fullPathToExampleFile)
        {
            if (!File.Exists(fullPathToExampleFile))
                throw new FileNotFoundException("The specified file does not exist");
            fullPath = fullPathToExampleFile;
        }

        public string Name
        {
            get
            {
                return Path.GetFileNameWithoutExtension(fullPath);
            }
        }

        public string Description
        {
            get
            {
                return GetDescription(fullPath);
            }
        }

        private string GetDescription(string fullPath)
        {
            using (StreamReader reader = new StreamReader(fullPath))
            {
                char[] buffer = new char[1024]; //Read 1 Kb of information from the file.
                reader.ReadBlock(buffer, 0, 1024);
                return new String(buffer);
            }
        }

        public string FullPath { get { return fullPath; } }

        string category;
        public string Category
        {
            get
            {
                if (category == null)
                {
                    InitializeCategoryFromFilePath();
                }
                return category;
            }
        }

        private void InitializeCategoryFromFilePath()
        {
            var dir = new DirectoryInfo(fullPath);
            this.subCategory = dir.Parent.Name;
            this.category = dir.Parent.Parent.Name;
        }

        string subCategory;
        public string SubCategory
        {
            get { if (subCategory == null) InitializeCategoryFromFilePath(); return subCategory; }
        }
    }
}
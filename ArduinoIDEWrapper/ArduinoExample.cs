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
                    category = ComputeCategoryFromFilePath();
                }
                return category;
            }
        }

        private string ComputeCategoryFromFilePath()
        {
            var examplesDir = "examples";
            var path = fullPath;
            do
            {
                var dir = Path.GetDirectoryName(path);
                if (String.Equals(dir, examplesDir, StringComparison.OrdinalIgnoreCase))
                {

                }
                path = Directory.GetParent(path).FullName;
            }
            while (string.IsNullOrEmpty(path));
        }
        public string SubCategory { get; private set; }
    }
}

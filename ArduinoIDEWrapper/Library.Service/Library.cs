using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Library
{
    public sealed class ArduinoLibrary
    {
        public ArduinoLibrary(string fullPath)
        {
            this.path = fullPath;
        }

        string name;
        public string Name
        {
            get
            {
                if (name == null)
                    name = System.IO.Path.GetFileNameWithoutExtension(this.path);
                return this.name;
            }
        }

        string path;
        public string Path { get { return this.path; } }

        IEnumerable<string> compilationUnits;
        public IEnumerable<string> CompilationUnits
        {
            get
            {
                if (this.compilationUnits == null)
                    InitializeCompilationUnits();
                return this.compilationUnits;
            }
        }


        IEnumerable<string> includes;
        public IEnumerable<string> IncludeFiles
        {
            get
            {
                if (includes == null)
                    InitializeIncludeFiles();
                return includes;
            }
        }

        public IEnumerable<Keyword> Keywords
        {
            get
            {
                var keywordFilePath = System.IO.Path.Combine(path, "keywords.txt");
                return EnumerateKeywords(keywordFilePath);
            }
        }

        private IEnumerable<Keyword> EnumerateKeywords(string keywordFilePath)
        {
            if (!File.Exists(keywordFilePath))
                yield break;

            using (StreamReader reader = new StreamReader(keywordFilePath))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    var tokens = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Count() == 2)
                        yield return new Keyword(tokens[0], tokens[1]);
                    else
                        continue;
                }
            }
        }

        private void InitializeIncludeFiles()
        {
            var srcPath = System.IO.Path.Combine(this.path, "src");
            includes = Directory.EnumerateFiles(this.Path, "*.h", SearchOption.AllDirectories);
            if (!includes.Any())
                this.includes = Enumerable.Empty<string>();
        }

        private void InitializeCompilationUnits()
        {
            var srcPath = System.IO.Path.Combine(this.path, "src");
            compilationUnits = Directory.EnumerateFiles(srcPath, "*.cpp", SearchOption.AllDirectories);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Example.Service
{
    public sealed class ExamplesService
    {
        string installBasePath;
        public ExamplesService(string installPath)
        {
            this.installBasePath = installPath;
        }

        public IEnumerable<ArduinoSketch> EnumerateExamples()
        {
            foreach (var item in EnumerateExamplesInternal().Union(EnumerateLibraryExamplesInternal()))
            {
                yield return item;
            }
        }

        //Return the example projects from the Arduino examples directory
        private IEnumerable<ArduinoSketch> EnumerateExamplesInternal()
        {
            var basePathToExamples = Path.Combine(installBasePath, "examples");

            if (!Directory.Exists(basePathToExamples))
                yield break;

            foreach (var file in Directory.EnumerateFiles(basePathToExamples, "*.ino", SearchOption.AllDirectories))
            {
                yield return new ArduinoExampleSketch(file);
            }
        }

        private IEnumerable<ArduinoSketch> EnumerateLibraryExamplesInternal()
        {
            var basePathToExamples = Path.Combine(installBasePath, "libraries");

            if (!Directory.Exists(basePathToExamples))
                yield break;

            foreach (var file in Directory.EnumerateFiles(basePathToExamples, "*.ino", SearchOption.AllDirectories))
            {
                yield return new ArduinoLibraryExampleSketch(file);
            }
        }
    }
}

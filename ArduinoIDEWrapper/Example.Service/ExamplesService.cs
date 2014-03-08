using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Arduino.IDE.Example.Service;

namespace Arduino.IDE.Example
{
    public sealed class ExamplesService : IServiceProvider<ExamplesService,string>
    {
        string installBasePath;
        public ExamplesService(string installPath)
        {
            this.installBasePath = installPath;
        }

        public IEnumerable<ArduinoSketch> EnumerateExamplesAll()
        {
            foreach (var item in EnumerateExamples().Union(EnumerateExamplesInLibrary()))
            {
                yield return item;
            }
        }

        //Return the example projects from the Arduino examples directory
        //Don't return if it's empty/null
        public IEnumerable<ArduinoSketch> EnumerateExamples()
        {
            var basePathToExamples = Path.Combine(installBasePath, "examples");

            if (!Directory.Exists(basePathToExamples))
                yield break;

            foreach (var file in Directory.EnumerateFiles(basePathToExamples, "*.ino", SearchOption.AllDirectories))
            {
                yield return new ArduinoExampleSketch(file);
            }
        }

        //Return the example projects from the Arduino library directory. Remember that the Arduino Library directories could be empty and might not ship an example !
        public IEnumerable<ArduinoSketch> EnumerateExamplesInLibrary()
        {
            var basePathToExamples = Path.Combine(installBasePath, "libraries");

            if (!Directory.Exists(basePathToExamples))
                yield break;

            foreach (var file in Directory.EnumerateFiles(basePathToExamples, "*.ino", SearchOption.AllDirectories))
            {
                yield return new ArduinoLibraryExampleSketch(file);
            }
        }

        ExamplesService service;
        public void Create(string args)
        {
            if (service == null)
                service = new ExamplesService(args);
        }

        public ExamplesService GetService()
        {
            if (service != null)
                return service;
            else
                throw new NullReferenceException("The requested service is not initialized yet.");
        }
    }
}

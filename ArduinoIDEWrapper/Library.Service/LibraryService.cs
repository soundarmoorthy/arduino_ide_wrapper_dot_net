using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Library
{
    public sealed class LibraryService : IServiceProvider<LibraryService, string>
    {
        string fullPathToInstallation;
        public LibraryService(string fullPathToInstallation)
        {
            this.fullPathToInstallation = fullPathToInstallation;
        }


        public IEnumerable<ArduinoLibrary> EnumerateLibraries()
        {
            var libraryBaseDir = Path.Combine(fullPathToInstallation, "libraries");
            foreach (var libraryPath in Directory.EnumerateDirectories(libraryBaseDir))
            {
                yield return new ArduinoLibrary(libraryPath);
            }
        }

        LibraryService service;
        public void Create(string args)
        {
            if (service == null)
                service = new LibraryService(args);
        }

        public LibraryService GetService()
        {
            if (service != null)
                return service;
            else
                throw new NullReferenceException("The requested service is not initialized.");
        }
    }
}

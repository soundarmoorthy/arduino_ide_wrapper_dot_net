using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Library
{
    public sealed class LibraryService
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
    }
}

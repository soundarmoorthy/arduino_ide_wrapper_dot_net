using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Library;
using Arduino.IDE;
using System.IO;

namespace ArduinoWrapper.Tests.Library.Service
{
    [TestClass]
    public sealed class LibraryTests
    {
        [TestMethod]
        public void SimpleTest_LibraryAccessors()
        {
            var samples = Directory.EnumerateDirectories(Path.Combine(ArduinoInstallationService.InstallationPath, "libraries"));

            foreach (var sample in samples)
            {
                var library = new ArduinoLibrary(sample);

                Assert.IsNotNull(library);
                Assert.IsNotNull(library.Path);
                Assert.IsNotNull(library.CompilationUnits);
                Assert.IsNotNull(library.IncludeFiles);
                Assert.IsNotNull(library.Name);

                foreach (var keyword in library.Keywords)
                {
                    Assert.IsNotNull(keyword);
                    Assert.IsNotNull(keyword.Key);
                    Assert.IsNotNull(keyword.Value);
                }
            }
        }
    }
}

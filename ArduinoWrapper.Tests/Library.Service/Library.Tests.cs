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
        [TestCategory("RequireInstallation")]
        public void Test_If_Library_Accessors_Works_Without_Exceptions()
        {
            var samples = Directory.EnumerateDirectories(Path.Combine(ArduinoInstallationService.InstallationPath, "libraries"));

            foreach (var sample in samples)
            {
                var library = new ArduinoLibrary(sample);

                Assert.IsNotNull(library);

                Assert.IsNotNull(library.Path);
                Assert.IsNotNull(Path.GetFullPath(library.Path));

                Assert.IsNotNull(library.CompilationUnits);
                Assert.IsTrue(library.CompilationUnits.Any()); //There should be atleast one compilation unit per library

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

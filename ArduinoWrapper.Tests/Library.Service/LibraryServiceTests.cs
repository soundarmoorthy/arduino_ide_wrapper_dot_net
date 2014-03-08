using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Example;
using Arduino.IDE;
using Arduino.IDE.Library;

namespace ArduinoWrapper.Tests.Library.Service
{
    /// <summary>
    /// Summary description for LibraryServiceTests
    /// </summary>
    [TestClass]
    public class LibraryServiceTests
    {
        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void Test_If_Library_Service_Enumerates_Libraries_Properly()
        {
            LibraryService service = new LibraryService(ArduinoInstallationService.InstallationPath);
            Assert.IsNotNull(service);
            foreach (var library in service.EnumerateLibraries())
            {
                Assert.IsNotNull(library);
            }
        }
    }
}

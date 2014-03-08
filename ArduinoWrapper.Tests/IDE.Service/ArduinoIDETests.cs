using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE;
using System.IO;

namespace ArduinoWrapper.Tests.IDE.Service
{
    [TestClass]
    public sealed class ArduinoIDETests
    {
        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void Test_If_The_IDE_Instance_Is_Created_Properly_And_All_Properties_Are_Are_Non_Null()
        {
            var ide = new ArduinoIDE(ArduinoInstallationService.InstallationPath);

            Assert.IsNotNull(ide);

            Assert.IsNotNull(ide.Version);
            Assert.IsNotNull(ide.ReleaseNotes);
        }

        [TestMethod]
        [TestCategory("RequireInstallation")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForUnexpectedEmptyString()
        {
            var ide = new ArduinoIDE(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForNull()
        {
            var ide = new ArduinoIDE(null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void TestForUnexpectedPath()
        {
            var randomDir = Path.GetDirectoryName(Path.GetRandomFileName());
            var ide = new ArduinoIDE(randomDir);

            try
            {

                var version = ide.Version;
            }
            catch (FileNotFoundException)
            {

                var releaseNotes = ide.ReleaseNotes;
            }

        }
    }
}

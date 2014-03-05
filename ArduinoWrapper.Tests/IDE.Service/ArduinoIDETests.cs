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
        public void TestForSurprises()
        {
            var ide = new ArduinoIDE(ArduinoInstallationService.InstallationPath);

            Assert.IsNotNull(ide);

            Assert.IsNotNull(ide.Version);
            Assert.IsNotNull(ide.ReleaseNotes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestForUnexpectedNull()
        {
            try
            {
            var ide = new ArduinoIDE(string.Empty);
            }
            catch(ArgumentException ex)
            {
                var ide = new ArduinoIDE(null);
            }

            Assert.Fail("Expected exception was not thrown");
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

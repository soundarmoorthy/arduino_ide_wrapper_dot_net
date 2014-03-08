using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE;
using System.IO;

namespace ArduinoWrapper.Tests
{
    [TestClass]
    public class InstallationServiceTests
    {
        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void TestForNonNull()
        {
            Assert.IsNotNull(ArduinoInstallationService.InstallationPath);
            Assert.IsTrue(Path.IsPathRooted(ArduinoInstallationService.InstallationPath));

            Assert.IsNotNull(ArduinoInstallationService.ExamplesDir);
            Assert.IsTrue(Path.IsPathRooted(ArduinoInstallationService.ExamplesDir));

            Assert.IsNotNull(ArduinoInstallationService.LibrariesDir);
            Assert.IsTrue(Path.IsPathRooted(ArduinoInstallationService.ExamplesDir));

            Assert.IsNotNull(ArduinoInstallationService.ReferenceDir);
            Assert.IsTrue(Path.IsPathRooted(ArduinoInstallationService.ReferenceDir));

            Assert.IsNotNull(ArduinoInstallationService.DriversDir);
            Assert.IsTrue(Path.IsPathRooted(ArduinoInstallationService.DriversDir));
        }

    }
}

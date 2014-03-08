using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Arduino.IDE;
using Arduino.IDE.Example;

namespace ArduinoWrapper.Tests.Example.Service.Tests
{
    [TestClass]
    public sealed class ArduinoExampleServiceTests
    {

        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void Test_If_Examples_Are_Enumerated_Without_Errors()
        {
            var exService = new ExamplesService(ArduinoInstallationService.InstallationPath);
            var examples = exService.EnumerateExamplesAll();

            foreach (var item in examples)
            {
                Assert.IsNotNull(item);
                //This is rather time consuming. But this is important because there are differences in how description is retrived depending
                //on the nature of the example project.
                Assert.IsNotNull(item.Context);
                Assert.IsNotNull(item.FullPath);
                Assert.IsNotNull(item.Name);

                var cat = item.Category;
                
                while(cat != null)
                {
                    Assert.IsNotNull(cat.Name);
                    cat = cat.Child;
                }

                //Debug.WriteLine(item);
            }
        }

        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void Test_If_Examples_In_Libraries_Are_Enumerated_Without_Errors()
        {
            ExamplesService exService = new ExamplesService(ArduinoInstallationService.InstallationPath);
            Assert.IsNotNull(exService);

            foreach (var example in exService.EnumerateExamplesInLibrary())
            {
                Assert.IsNotNull(example);
            }
        }

        [TestMethod]
        [TestCategory("RequireInstallation")]
        public void Test_If_Examples_Alone_Are_Enumerated_Without_Errors()
        {
            ExamplesService exService = new ExamplesService(ArduinoInstallationService.InstallationPath);
            Assert.IsNotNull(exService);

            foreach (var example in exService.EnumerateExamplesInLibrary())
            {
                Assert.IsNotNull(example);
            }
        }
    }
} 
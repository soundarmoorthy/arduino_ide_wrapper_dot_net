using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Arduino.IDE;
using Arduino.IDE.Example;
using System.Linq;
using System;

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

        [TestMethod]
        public void Test_If_Enumerate_Examples_Run_Without_Errors_Even_If_Directory_Doesnt_Exist()
        {
            var dir = Path.Combine(Path.GetTempPath(), DateTime.Now.ToFileTime().ToString());
            var service = new ExamplesService(dir);

            Assert.IsNotNull(service);
            //Actually, the following code doens't do anything but to unfold the enumerable you have to iterate them over.
            foreach (var example in service.EnumerateExamplesAll())
            {

                Assert.IsNotNull(example);
            }

            
        }
    }
} 
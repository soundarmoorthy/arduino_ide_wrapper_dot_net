using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Example.Service;
using System.IO;
using Arduino.IDE;
using System.Diagnostics;

namespace ArduinoWrapper.Tests.Example.Service.Tests
{
    [TestClass]
    public sealed class ArduinoExampleSketchTests
    {
        [TestMethod]
        public void EnumerateExamplesTests()
        {
            var exService = new ExamplesService(EnvironmentService.InstallationPath);
            var examples = exService.EnumerateExamples();

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
    }
} 
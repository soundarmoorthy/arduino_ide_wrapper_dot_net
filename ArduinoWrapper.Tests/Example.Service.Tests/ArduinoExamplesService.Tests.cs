using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Example.Service;
using System.IO;
using Arduino.IDE;

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
                Assert.IsNotNull(item.Description);
                Assert.IsNotNull(item.FullPath);
                Assert.IsNotNull(item.Name);

                var cat = item.Category;
                
                while(cat != null)
                {
                    Assert.IsNotNull(cat.Name);
                    cat = cat.Child;
                }
            }
        }
    }
} 
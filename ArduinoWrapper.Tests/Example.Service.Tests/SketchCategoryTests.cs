using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Example;

namespace ArduinoWrapper.Tests.Example.Service.Tests
{
    [TestClass]
    public class SketchCategoryTests
    {
        [TestMethod]
        public void Sketch_Category_Works_Without_Errors()
        {
            
            var sketchCategory = new SketchCategory();
            Assert.IsTrue(sketchCategory.ToString() != null);
        }
    }
}

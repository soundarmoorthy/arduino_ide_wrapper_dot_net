using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Arduino.IDE.Example;

namespace ArduinoWrapper.Tests.Example.Service.Tests
{
    [TestClass]
    public class ArduinoLibraryExampleSketchTests
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Argument_Is_Empty()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoLibraryExampleSketch(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Argument_Is_Null()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoLibraryExampleSketch(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Doesnt_Exist()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoLibraryExampleSketch(@"m:\foo\bar\doo");
        }
    }
}

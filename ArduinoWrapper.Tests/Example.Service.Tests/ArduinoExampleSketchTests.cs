using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE.Example;
using System.IO;
using Arduino.IDE;

namespace ArduinoWrapper.Tests.Example.Service.Tests
{
    [TestClass]
    public class ArduinoExampleSketchTests
    {

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Argument_Is_Empty()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoExampleSketch(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Argument_Is_Null()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoExampleSketch(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Throws_FileNotFoundException_When_File_Doesnt_Exist()
        {
            ArduinoSketch sketch;
            sketch = new ArduinoExampleSketch(@"m:\foo\bar\doo");
        }
    }
}

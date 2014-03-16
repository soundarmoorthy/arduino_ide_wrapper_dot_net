using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arduino.IDE;
using System.IO;
using Arduino.IDE.Board.Service;

namespace ArduinoWrapper.Tests.Board.Service.Tests
{
    [TestClass]
    public class BoardServiceTests
    {
        [TestMethod]
        public void Test_If_Board_Service_Initializes_Properly()
        {
            var boardService = new BoardService(ArduinoInstallationService.InstallationPath);
            Assert.IsNotNull(boardService);
        }

        [TestMethod]
        public void Test_If_Comments_In_Properties_File_Are_Skipped()
        {

            #region Prepare the Properties file without comments
            string propertiesFileContent = "#ExamplePropertiesFile\ncom.atmel.avr.simulator=debugger\n   #Simulator path\ncom.atmel.simulator.path = #SIM_PATH#";
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, propertiesFileContent);
            #endregion

            var boardService = new BoardService();
            var properties = boardService.ParsePropertiesFile(tempFile);

        }
    }
}
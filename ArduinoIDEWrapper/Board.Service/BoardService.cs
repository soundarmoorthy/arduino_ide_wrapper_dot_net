using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE.Board.Service
{
    public sealed class BoardService : IServiceProvider<BoardService, string>
    {

        string basePath;
        public BoardService(string basePathOfArduinoInstallation)
        {
            basePath = basePathOfArduinoInstallation;
        }

        public IEnumerable<ArduinoBoard> EnumerateBoards()
        {


        }
    }
}

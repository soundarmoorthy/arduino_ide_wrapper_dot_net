using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Arduino.IDE
{
    public static class ArduinoInstallationService
    {
        [XmlElement()]
        public static string InstallationPath { get { return @"C:\interopArduino\arduino-1.5.6-r2\"; } }

        [XmlElement()]
        public static string ExamplesDir { get { return InstallationPath + "examples"; }  }

        [XmlElement()]
        public static string LibrariesDir { get { return InstallationPath + "libraries"; }  }

        [XmlElement()]
        public static string ReferenceDir { get { return InstallationPath + "reference"; }  }

        [XmlElement()]
        public static string DriversDir { get { return InstallationPath + "drivers"; }  }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE
{
    public sealed class ArduinoIDE
    {
        public ArduinoIDE(string installBasePath)
        {
            if (string.IsNullOrEmpty(installBasePath))
                throw new ArgumentException("installBasePath");

            if (!Directory.Exists(installBasePath))
                throw new DirectoryNotFoundException("The specified installation base directory doesn't exist");

            this.installBasePath = installBasePath;
        }
        public Version Version
        {
            get
            {
                if (version == null)
                    version = InitializeVersion();
                return version;
            }
        }
        public StreamReader ReleaseNotes
        {
            get
            {
                var path = Path.Combine(installBasePath, "revisions.txt");
                if (File.Exists(path))
                    return new StreamReader(path);
                else
                    throw new FileNotFoundException(string.Format("Unable to find the file {0} to retrieve release notes"));
            }
        }


        private string installBasePath;
        private Version version;
        private Version InitializeVersion()
        {
            var path = Path.Combine(installBasePath, "lib", "version.txt");
            if (File.Exists(path))
            {
                var version = File.ReadAllText(path);
                return new Version(version);
            }
            else
                throw new FileNotFoundException(string.Format("Unable to find the file {0} to retrieve the revision information of the Arduino installation", path));
        }

    }
}
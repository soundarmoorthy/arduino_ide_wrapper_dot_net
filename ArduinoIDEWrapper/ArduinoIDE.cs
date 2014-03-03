using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Wrapper
{
    public sealed class ArduinoIDE
    {
        string installBasePath;
        public ArduinoIDE(string installBasePath)
        {
            if (string.IsNullOrEmpty(installBasePath))
                throw new ArgumentException("installBasePath");

            if (!Directory.Exists(installBasePath))
                throw new DirectoryNotFoundException("The specified installation base directory doesn't exist");

            this.installBasePath = installBasePath;
        }

        private Version version;
        public Version Version
        {
            get
            {
                if (version == null)
                {
                    version = InitializeVersion();
                }
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

        public IEnumerable<ArduinoExample> EnumerateExamples()
        {
            var basePathToExamples = Path.Combine(installBasePath, "examples");
            if (!Directory.Exists(basePathToExamples))
                return Enumerable.Empty<ArduinoExample>();

            var examples = RecurseEnumerateExamples(basePathToExamples);
        }

        private IEnumerable<ArduinoExample> RecurseEnumerateExamples(string examplesDirectory)
        {
            foreach (var file in Directory.EnumerateFiles(examplesDirectory, "*.ino", SearchOption.AllDirectories))
            {
                yield return new ArduinoExample(file);
            }
        }


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
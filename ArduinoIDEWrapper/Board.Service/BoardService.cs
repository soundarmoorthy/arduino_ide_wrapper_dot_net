using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Arduino.IDE.Board.Service
{
    public sealed class BoardService : IServiceProvider<BoardService, string>
    {
        string basePath;
        IEnumerable<ArduinoBoard> boards;
        internal BoardService(string basePathOfArduinoInstallation)
        {
            if (string.IsNullOrEmpty(basePath))
                throw new ArgumentNullException("Base Path cannot be null or empty");

            if (!Directory.Exists(basePath))
                throw new DirectoryNotFoundException("The Boards Directory cannot be found");

            basePath = basePathOfArduinoInstallation;
        }

        internal BoardService()
        {

        }

        public IEnumerable<ArduinoBoard> EnumerateBoards()
        {
            var path = Path.Combine(basePath, "hardware", "arduino", "avr");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("Unable to enumerate the boards. The board definition file is not available in the Arduino Installation. Expected Boards.txt in {0}", path));
            }

            if (boards == null)
                DiscoverBoards(path);
            return boards;
        }

        private void DiscoverBoards(string path)
        {
            var properties = ParsePropertiesFile(path);
            this.boards = DiscoverBoards(properties);
        }

        private IEnumerable<ArduinoBoard> DiscoverBoards(IEnumerable<ArduinoProperty> properties)
        {
            return Enumerable.Empty<ArduinoBoard>();
        }

        enum Context
        {
            Start,
            Comment,
            PropertyName,
            PropertyValueBeforeWhitespaceRemoval,
            PropertyValue
        }

        //This will parse a .properties file and will return the properties in it as a Dictionary
        //To understand this properly you could perhaps read http://docs.oracle.com/javase/6/docs/api/java/util/Properties.html#load%28java.io.Reader%29
        internal List<ArduinoProperty> ParsePropertiesFile(string fullPathToBoardsFile)
        {
            List<ArduinoProperty> properties = new List<ArduinoProperty>();
            using (StreamReader sr = new StreamReader(fullPathToBoardsFile))
            {
                //Read each line and look for properties
                char ch;
                int read;
                LinkedList<char> propertyName = new LinkedList<char>();
                LinkedList<char> propertyValue = new LinkedList<char>();
                Context context = Context.Start;
                //Finite automation to parse the properties file.
                while ((read = sr.Read()) != -1)
                {
                    ch = Convert.ToChar(read);
                    switch(context)
                    {
                        case Context.Start:
                            //From this state transition can happen to the current state or a comment or a property name.
                            if (char.IsWhiteSpace(ch))
                            {
                                continue; //Just ignore and continue
                            }

                            if (ch == '#' || ch == '!') //# or ! as first character of a line signifies a comment.
                            {
                                context = Context.Comment;
                            }
                            else
                            {
                                context = Context.PropertyName;
                                propertyName.AddLast(ch);
                            }
                            break;

                        case Context.Comment:
                            if (ch == '\n')
                            {
                                //Check if the previous character is a continuation character
                                context = Context.Start;
                            }
                            break;

                        case Context.PropertyName:
                            if (ch == '=' || ch == ':') //The first occurance of either = or : is the separator for property key and property value.
                            {
                                //If these characters are escaped inside properties, then they should be considered as part of the property name.
                                if (propertyName.Last.Value == '\\')
                                {
                                    propertyName.RemoveLast();
                                    propertyName.AddLast(ch);
                                }
                                else
                                    context = Context.PropertyValueBeforeWhitespaceRemoval;
                            }

                            else if (ch == '\n')
                            {
                                //Now decide if there is a backslash escaping the line termination. Remember that there can be escape sequence as part of 
                                //the property name. To determine that, find that if there are odd number of '\' characters.
                                int count = 0;
                                var lastChar = propertyName.Last;
                                while (lastChar.Value != '\\')
                                {
                                    count++;
                                    lastChar = lastChar.Previous;
                                }
                                if (count == 0)
                                    throw new Exception("Invalid property");
                                else if (count % 2 == 1)
                                {
                                    //If it's a genuine line continuation then we should perhaps remove the 
                                    propertyName.RemoveLast();
                                    context = Context.Start;
                                    continue;
                                }
                            }
                            else
                            {
                                propertyName.AddLast(ch);
                            }
                            break;

                        case Context.PropertyValueBeforeWhitespaceRemoval:
                            //After the = sign in a line trim all the whitespaces before consdering the property value.
                            if (char.IsWhiteSpace(ch))
                            {
                                continue;
                            }
                            context = Context.PropertyValue;
                            break;

                        case Context.PropertyValue:
                            if (ch == '\n')
                            {
                                //Do the same stuff as we did for property name.
                                int count = 0;
                                var lastChar = propertyName.Last;
                                while (lastChar.Value != '\\')
                                {
                                    count++;
                                    lastChar = lastChar.Previous;
                                }
                                if (count == 0)
                                {
                                    //If we are here, then it's probably that a line is parsed succesfully and that we are about to go to the next line.
                                    //So save the parsed key value pair here.
                                    properties.Add(new ArduinoProperty(propertyName, propertyValue));
                                    propertyName.Clear();
                                    propertyValue.Clear();
                                    context = Context.Start;
                                }
                                else if (count % 2 == 1)
                                {
                                    //If it's a genuine line continuation then we should perhaps remove the 
                                    propertyName.RemoveLast();
                                    context = Context.PropertyValueBeforeWhitespaceRemoval;
                                    continue;
                                }
                            }
                            else
                            {
                                propertyValue.AddLast(ch);
                            }
                            break;
                    }
                }
            }
            return properties;
        }

        BoardService service;
        public void Create(string args)
        {
            if (service == null)
                service = new BoardService(args);
        }

        public BoardService GetService()
        {
            return service;
        }
    }
}
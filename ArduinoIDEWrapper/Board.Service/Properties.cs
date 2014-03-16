using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arduino.IDE.Board.Service
{
    public sealed class ArduinoProperty
    {
        public ArduinoProperty()
        {
            
        }

        //When the parsing of the properties are done from the 
        //.properties file, we represent them as LinkedList<char>
        // because it's convinent to traverse the context back and forth
        //These will be converted to string when accessed, which at best case is not needed :-).
        LinkedList<char> name;
        LinkedList<char> value;

        internal ArduinoProperty(string name, string value){}

        internal ArduinoProperty(LinkedList<char> name, LinkedList<char> value)

        {
            this.name = name;
            this.value = value;
        }
    }
}

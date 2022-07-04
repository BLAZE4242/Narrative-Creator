using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class define
    {
        public static Dictionary<string, string> stringVariables = new Dictionary<string, string>();
        public static Dictionary<string, float> floatVariables = new Dictionary<string, float>();
        public static Dictionary<string, string> configVariables = new Dictionary<string, string>()
        {
            { "choiceColour", "blue" },
            { "textColour", "white" },
            { "clearConsoleAfterChoice", "true" }
        };

        public static ConsoleColor stringToColour(string colour)
        {
            switch (colour)
            {
                case "black":
                    return ConsoleColor.Black;
                case "blue":
                    return ConsoleColor.Blue;
                case "cyan":
                    return ConsoleColor.Cyan;
                case "darkBlue":
                    return ConsoleColor.DarkBlue;
                case "darkCyan":
                    return ConsoleColor.DarkCyan;
                case "darkGray":
                    return ConsoleColor.DarkGray;
                case "darkGreen":
                    return ConsoleColor.DarkGreen;
                case "darkMagenta":
                    return ConsoleColor.DarkMagenta;
                case "darkRed":
                    return ConsoleColor.DarkRed;
                case "darkYellow":
                    return ConsoleColor.DarkYellow;
                case "gray":
                    return ConsoleColor.Gray;
                case "green":
                    return ConsoleColor.Green;
                case "magenta":
                    return ConsoleColor.Magenta;
                case "red":
                    return ConsoleColor.Red;
                case "white":
                    return ConsoleColor.White;
                case "yellow":
                    return ConsoleColor.Yellow;
                default:
                    throw new Exception(colour + " is not a real colour");
            }



        }

        public void defineVariable(string line)
        {
            TextInterpreter _interpreter = new TextInterpreter();

            string[] arguments = _interpreter.SplitByString(line, " || ");

            string variableType = arguments[1];

            if (variableType == "string")
            {
                defineString(arguments);
            }
            else if (variableType == "float")
            {
                defineFloat(arguments);
            }
        }

        void defineString(string[] arguments)
        {
            string variableName = arguments[2]; // wasd
            string variableValue = arguments[3]; // hello

            stringVariables[variableName] = variableValue;
        }

        void defineFloat(string[] arguments)
        {
            string variableName = arguments[2];

            if (arguments[3] == "+")
            {
                floatVariables[variableName] += float.TryParse(arguments[4], out var variableValueAddition) ? variableValueAddition : 1;
            }
            else if (arguments[3] == "-")
            {
                floatVariables[variableName] -= float.TryParse(arguments[4], out var variableValueAddition) ? variableValueAddition : -1;
            }
            else
            {
                floatVariables[variableName] = float.TryParse(arguments[3], out var variableValue) ? variableValue : 0;
            }
        }
    }
}

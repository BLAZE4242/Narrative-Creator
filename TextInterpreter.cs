using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

namespace Test_Narritive
{
    class TextInterpreter
    {
        ChoicesLogic _choice = new ChoicesLogic();

        public void ReadLines(string fileName, bool clearScreen = true)
        {
            if (clearScreen)
            {
                scriptHistory = "";
                Console.Clear();
            }

            string[] scriptContent = scriptText(fileName);
            
            for(int i = 0; i < scriptContent.Length; i++)
            {
                if (string.IsNullOrEmpty(scriptContent[i].Trim()) || scriptContent[i].StartsWith("//"))
                {
                    continue;
                }

                if (interpretLine(scriptContent[i]))
                {
                    if (!scriptContent[i + 2].StartsWith("$ choice")) // has to be +2 because there (should be) a line gap between last text and "$ choice" and I'm too lazy to program this logic
                    {
                        WaitForInput();
                    }
                }
            }
        }

        bool isInChoiceLoop = false;

        bool isInIfLoop = false; // wtf
        bool isConditionTrue = false;

        int changeTempColour = 0;
        string oldColour;

        Dictionary<string, string> choiceAction = new Dictionary<string, string>();
        public static string scriptHistory;
        bool interpretLine(string line) // returns true if can read line
        {
            if (line.StartsWith("$ choice"))
            {
                isInChoiceLoop = true;
            }
            else if (line.StartsWith("$ endchoice"))
            {
                isInChoiceLoop = false;
                Console.WriteLine("");
                _choice.MakeChoice(choiceAction);
            }
            else if (isInChoiceLoop)
            {
                choiceAction.Add(SplitByString(line, " || ")[0], SplitByString(line, " || ")[1]);
            }
            else if (line.StartsWith("$ if"))
            {
                isInIfLoop = true;
                isConditionTrue = IfCondition.IsConditionTrue(SplitByString(line, " || "));
            }
            else if(line.StartsWith("$ endif"))
            {
                isInIfLoop = false;
                isConditionTrue = false;
            }
            else if(isInIfLoop && !isConditionTrue)
            {
                return false;
            }
            else if (line.StartsWith("$ goto"))
            {
                new UserSelectsChoice().OnUserMakesChoice(SplitByString(line, " || ")[1], false);
                return false;
            }
            else if (line.StartsWith("$ wait"))
            {
                Thread.Sleep(int.TryParse(SplitByString(line, " || ")[1], out var timeToWait) ? timeToWait * 1000 : 0); // ? operator is for if statements, if_true : if_false
                return false;
            }
            else if(line.StartsWith("$ colour")) // L for us
            {
                changeTempColour = 1;
                oldColour = define.configVariables["textColour"];
                define.configVariables["textColour"] = SplitByString(line, " || ")[1];
            }
            else if (line.StartsWith("$ define"))
            {
                new define().defineVariable(line);
            }
            else
            {
                if (changeTempColour > 1)
                {
                    define.configVariables["textColour"] = oldColour;
                }
                else if(changeTempColour > 0)
                {
                    changeTempColour++;
                }
                return WriteText(line, define.stringToColour(define.configVariables["textColour"]));
            }

            return false;
        }

        bool WriteText(string line, ConsoleColor colour)
        {
            string lineToPrint = line;
            foreach (string key in define.stringVariables.Keys)
            {
                lineToPrint = lineToPrint.Replace("{" + key + "}", define.stringVariables[key]);
            }
            foreach (string key in define.floatVariables.Keys)
            {
                lineToPrint = lineToPrint.Replace("{" + key + "}", define.floatVariables[key].ToString());
            }

            scriptHistory += lineToPrint + "\n";
            Console.ForegroundColor = colour;
            Console.WriteLine(lineToPrint);
            return true;
        }

        void WaitForInput()
        {
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                {
                    return;
                }
            }
            
        }

        string[] scriptText(string fileName)
        {
            return File.ReadAllLines(fileName);
        }

        public string pathOfExe()
        {
            List<string> listPath = Assembly.GetExecutingAssembly().Location.Split('\\').ToList<string>();
            listPath.RemoveAt(listPath.Count - 1);
            string path = "";
            foreach (string line in listPath)
            {
                path += line + "\\";
            }

            return path;
        }

        public string[] SplitByString(string str, string split)
        {
            return str.Split(new string[] { split }, StringSplitOptions.None);
        }

    }
}

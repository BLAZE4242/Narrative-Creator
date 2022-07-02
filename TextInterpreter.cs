using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class TextInterpreter
    {
        ChoicesLogic _choice = new ChoicesLogic();

        public void ReadLines(string fileName)
        {
            string[] scriptContent = scriptText(fileName);
            
            foreach (string line in scriptContent)
            {
                if (string.IsNullOrEmpty(line.Trim()) || line.StartsWith("//"))
                {
                    continue;
                }

                interpretLine(line);
                if(line != "Do you understand?") WaitForInput();
            }
        }

        bool isInChoiceLoop = false;
        Dictionary<string, string> choiceAction = new Dictionary<string, string>();
        public static string scriptHistory;
        void interpretLine(string line)
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
            else
            {
                scriptHistory += line + "\n";
                Console.WriteLine(line);
            }
        }

        void WaitForInput()
        {
            bool canCheckForInput = true;
            while (canCheckForInput)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        string[] scriptText(string fileName)
        {
            List<string> listPath = Assembly.GetExecutingAssembly().Location.Split('\\').ToList<string>();
            listPath.RemoveAt(listPath.Count - 1);
            string path = "";
            foreach(string line in listPath)
            {
                path += line + "\\";
            }

            return File.ReadAllLines(path + "\\Scripts\\" + fileName + ".txt");
        }

        string[] SplitByString(string str, string split)
        {
            return str.Split(new string[] { split }, StringSplitOptions.None);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class ChoicesLogic
    {
        ConsoleColor selectedColour()
        {
            return define.stringToColour(define.configVariables["choiceSelectColour"]);
        }

        List<ConsoleColor> defaultColours(bool firstHighlighted = true)
        {
            List<ConsoleColor> tempList = new List<ConsoleColor>();
            for (int i = 0; i < currentOptions.Count; i++)
            {
                if (i == 0 && firstHighlighted)
                {
                    tempList.Add(selectedColour());
                }
                else
                {
                    tempList.Add(ConsoleColor.Black);
                }
            }
            return tempList;
        }
        List<ConsoleColor> optionColours(int selectedIndex)
        {
            List<ConsoleColor> tempList = defaultColours(false);
            tempList[selectedIndex] = selectedColour();
            return tempList;
        }
        List<ConsoleColor> currentColours = new List<ConsoleColor>();

        List<string> currentOptions = new List<string>();

        int userSelectedOption = 0;
        bool hasUserSelected = false;


        public void MakeChoice(Dictionary<string, string> options)
        {
            string[] optionsText = options.Keys.ToArray();

            currentOptions = new List<string>();
            foreach (string option in optionsText)
            {
                currentOptions.Add(option);
            }

            RenderOptions();
            CheckForInput(options);
        }

        void CheckForInput(Dictionary<string, string> options)
        {
            while (!hasUserSelected)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    OnUserInput(-1, options);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    OnUserInput(1, options);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    OnUserInput(0, options);
                }
            }
        }

        void OnUserInput(int input, Dictionary<string, string> options)
        {
            userSelectedOption += input;

            if (input == 0)
            {
                hasUserSelected = true;
                new UserSelectsChoice().OnUserMakesChoice(options.Values.ToArray()[userSelectedOption]);
                return;
            }

            if (userSelectedOption < 0)
            {
                userSelectedOption = currentOptions.Count-1;
            }
            else if (userSelectedOption > currentOptions.Count - 1)
            {
                userSelectedOption = 0;
            }

            currentColours = optionColours(userSelectedOption);
            RenderOptions(true);
        }

        void RenderOptions(bool clearConsole = false)
        {
            if (clearConsole)
            {
                Console.Clear();
                foreach(KeyValuePair<string, ConsoleColor> line in TextInterpreter.scriptHistory)
                {
                    Console.ForegroundColor = line.Value;
                    Console.WriteLine(line.Key);
                }
                Console.WriteLine("");
                Console.ForegroundColor = define.stringToColour(define.configVariables["textColour"]);
            }



            if(currentColours.Count == 0)
            {
                currentColours = defaultColours();
            }

            for (int i = 0; i < currentOptions.Count; i++)
            {
                Console.BackgroundColor = currentColours[i];
                Console.ForegroundColor = define.stringToColour(define.configVariables["textColour"]);
                Console.WriteLine($"{i + 1}: {currentOptions[i]}");
            }

            Console.ResetColor();
        }
    }
}

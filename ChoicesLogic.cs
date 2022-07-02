using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class ChoicesLogic
    {
        ConsoleColor selectedColour = ConsoleColor.Blue;

        List<ConsoleColor> defaultColours(bool firstHighlighted = true)
        {
            List<ConsoleColor> tempList = new List<ConsoleColor>();
            for (int i = 0; i < currentOptions.Count; i++)
            {
                if (i == 0 && firstHighlighted)
                {
                    tempList.Add(selectedColour);
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
            tempList[selectedIndex] = selectedColour;
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
            CheckForInput();
        }

        void CheckForInput()
        {
            while (!hasUserSelected)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    OnUserInput(false);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    OnUserInput(true);
                }
            }
        }

        void OnUserInput(bool wasDown)
        {
            if (wasDown) userSelectedOption++;
            else userSelectedOption--;

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
                Console.WriteLine(TextInterpreter.scriptHistory);
            }

            if(currentColours.Count == 0)
            {
                currentColours = defaultColours();
            }

            for (int i = 0; i < currentOptions.Count; i++)
            {
                Console.BackgroundColor = currentColours[i];
                Console.WriteLine($"{i + 1}: {currentOptions[i]}");
            }

            Console.ResetColor();
        }
    }
}

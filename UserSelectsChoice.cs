using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class UserSelectsChoice
    {
        public void OnUserMakesChoice(string fileName, bool clearScreen = true)
        {
            TextInterpreter _interpret = new TextInterpreter();
            _interpret.ReadLines(GetPath(fileName), clearScreen);
        }

        public string GetPath(string fileName)
        {
            foreach (string fileDir in Directory.GetFiles(new TextInterpreter().pathOfExe(), "*.txt*", SearchOption.AllDirectories))
            {
                if (fileDir.EndsWith(fileName + ".txt"))
                {
                    return fileDir;
                }
            }

            throw new Exception($"File {fileName} does not exist");
        }
    }
}

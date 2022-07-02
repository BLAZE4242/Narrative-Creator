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
        public void ReadLines(string fileName)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().Location + "\\");
            Console.ReadLine();
        }
    }
}

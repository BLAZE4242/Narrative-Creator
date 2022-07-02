using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class Program
    {
        

        public static void Main(string[] args)
        {
            TextInterpreter _interpret = new TextInterpreter();
            _interpret.ReadLines("main");

            //ChoicesLogic _choice = new ChoicesLogic();
            //_choice.MakeChoice("Choice one?", "Choice two!", "This might be choice 3", "is this one 4 I kinda forgot", "A 5th option????");
        }

        
    }
}

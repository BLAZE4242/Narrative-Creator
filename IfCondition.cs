using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Narritive
{
    class IfCondition
    {
        public static bool IsConditionTrue(string[] arguments)
        {
            string variableType = arguments[1];
            string variableName = arguments[2];
            string conditionValue = arguments[3];

            if (variableType == "string")
            {
                return define.stringVariables[variableName] == conditionValue;
            }

            throw new Exception("not a string");
        }
    }
}

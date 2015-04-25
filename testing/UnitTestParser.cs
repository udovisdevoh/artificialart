using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Parsing;

namespace testing
{
    static class UnitTestParser
    {
        internal static void TestAll()
        {
            UniversalParser universalParser = new UniversalParser();
            universalParser.AddBracketPriority('{', '}');
            universalParser.AddBracketPriority('[', ']');
            universalParser.AddBracketPriority('(', ')');
            universalParser.AddOperatorPriority(new string[] { "+", "-" });
            universalParser.AddOperatorPriority(new string[] { "*", "/" });
            universalParser.AddOperatorPriority("^");
            universalParser.DefaultBracketConcatenationOperator = "*";

            AssertEquals(universalParser.Parse("6+(1+2)*5+7*2+2^4").ToString(true), "+\r\n\t6\r\n\t+\r\n\t\t*\r\n\t\t\t+\r\n\t\t\t\t1\r\n\t\t\t\t2\r\n\t\t\t5\r\n\t\t+\r\n\t\t\t*\r\n\t\t\t\t7\r\n\t\t\t\t2\r\n\t\t\t^\r\n\t\t\t\t2\r\n\t\t\t\t4\r\n");
        }
        
        private static void AssertEquals(string string1, string string2)
        {
            if (string1 != string2)
                throw new Exception("Wrong value: " + string1 + " should be: " + string2);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testing
{
    static class UnitTests
    {
        public static void TestAll()
        {
            UnitTestParser.TestAll();
            UnitTestDeriver.TestAll();
            UnitTestMusic.TestAll();
            UnitTestLyricGenerator.TestAll();
            UnitTestTranslationBots.TestAll();
            UnitTestsLinguistics.TestAll();
            UnitTestWebServices.TestAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.WebServices;

namespace testing
{
    static class UnitTestTranslationBots
    {
        #region Public Methods
        public static void TestAll()
        {
            BabelFishTranslationBot babelFishTranslationBot = new BabelFishTranslationBot();
            AssertEquals(babelFishTranslationBot.Translate("I'm a banana", "en", "es"), "Soy un plátano");
            AssertEquals(babelFishTranslationBot.Translate("Soy un plátano", "es", "fr"), "Je suis une banane");
            AssertEquals(babelFishTranslationBot.Translate("I am a banana", "en", "fr"), "Je suis une banane");
            AssertEquals(babelFishTranslationBot.Translate("I’m a banana", "En", "fR"), "Je suis une banane");
            AssertEquals(babelFishTranslationBot.Translate("I'm a banana", "en", "fr"), "Je suis une banane");
            
            List<string> sourceList = new List<string>() { "I am a banana", "I'm an apple", "I'm a potato" };
            IList<string> translatedList = babelFishTranslationBot.Translate(sourceList, "en", "fr");

            AssertEquals(translatedList[0], "Je suis une banane");
            AssertEquals(translatedList[1], "Je suis une pomme");
            AssertEquals(translatedList[2], "Je suis une pomme de terre");
        }
        #endregion

        #region Private Methods
        private static void AssertEquals(string string1, string string2)
        {
            if (string1 != string2)
                throw new Exception("Wrong value: " + string1 + " should be: " + string2);
        }
        #endregion
    }
}

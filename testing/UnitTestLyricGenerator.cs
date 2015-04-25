using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.Lyrics;

namespace testing
{
    static class UnitTestLyricGenerator
    {
        #region Public Methods
        public static void TestAll()
        {
            LyricSongFactory songFactory = new LyricSongFactory("./","en");
            songFactory.AddTheme("activism");
            songFactory.AddTheme("fantastic");
            songFactory.AddTheme("philosophy");
            songFactory.AddTheme("geek");
            songFactory.AddTheme("metaphysics");
            songFactory.BarCount = 33;
            songFactory.BarCountPerChorus = 4;
            songFactory.SetBarSettings(0, 0f, 0);
            songFactory.SetBarSettings(1, 0.1f, 28);
            songFactory.SetBarSettings(2, 0.15f, 28);
            songFactory.SetBarSettings(3, 0.20f, 28);
            songFactory.SetBarSettings(4, 0.20f, 28);
            songFactory.SetBarSettings(5, 1f, 32);
            songFactory.SetBarSettings(6, 1f, 16);
            songFactory.SetBarSettings(7, 1f, 24);
            songFactory.SetBarSettings(8, 1f, 24);
            songFactory.SetBarSettings(9, 0.30f, 32);
            songFactory.SetBarSettings(10, 0.35f, 32);
            songFactory.SetBarSettings(11, 0.40f, 32);
            songFactory.SetBarSettings(12, 0.40f, 32);
            songFactory.SetBarSettings(13, 1f, 32);
            songFactory.SetBarSettings(14, 1f, 16);
            songFactory.SetBarSettings(15, 1f, 24);
            songFactory.SetBarSettings(16, 1f, 24);
            songFactory.SetBarSettings(17, 0.60f, 36);
            songFactory.SetBarSettings(18, 0.70f, 36);
            songFactory.SetBarSettings(19, 0.80f, 36);
            songFactory.SetBarSettings(20, 0.80f, 36);
            songFactory.SetBarSettings(21, 0f, 0);
            songFactory.SetBarSettings(22, 0f, 0);
            songFactory.SetBarSettings(23, 0f, 0);
            songFactory.SetBarSettings(24, 0f, 0);
            songFactory.SetBarSettings(25, 1f, 32);
            songFactory.SetBarSettings(26, 1f, 16);
            songFactory.SetBarSettings(27, 1f, 24);
            songFactory.SetBarSettings(28, 1f, 24);
            songFactory.SetBarSettings(29, 1f, 32);
            songFactory.SetBarSettings(30, 1f, 16);
            songFactory.SetBarSettings(31, 1f, 24);
            songFactory.SetBarSettings(32, 1f, 24);

            foreach (string themeName in songFactory.SelectableThemeNameList)
            {
                Console.WriteLine(themeName);
            }

            songFactory.LanguageCode = "fr";

            IEnumerable<string> song = songFactory.Build();
            foreach (string line in song)
            {
                Console.WriteLine(line);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArtificialArt.WebServices;

namespace testing
{
    static class UnitTestWebServices
    {
        public static void TestAll()
        {
            TestGoogleBot();
            TestWebBot();
        }

        private static void TestGoogleBot()
        {
            GoogleChatBot googleBot = new GoogleChatBot();
            googleBot.Search("C'est très fucké");
            googleBot.Search("C'est très étrange");
        }

        private static void TestWebBot()
        {
            WebBot webBot = new WebBot();
            string content = webBot.GetPageContent("http://www.anticulture.net");
            string expectedContent = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html4/loose.dtd\">\r\n<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=iso-8859-1\">\r\n<meta name=\"language\" content=\"en\">\r\n<meta name=\"country\" content=\"ca\">\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"Styles.css\">\r\n<link rel=\"shortcut icon\" href=\"favicon.ico\">\r\n<title>Random generators</title>\r\n</head>\r\n<body>\r\n\r\n\t\t\t<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"width:100%;background-color:#000\">\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\"Top\" style=\"width:213px\">\n\t\t\t\t\t\t<img src=\"Images/PrintedCircuits-001.gif\" alt=\"Music Generator\">\n\t\t\t\t\t</td>\n\t\t\t\t\t<td class=\"Top\" align=\"center\">\n\t\t\t\t\t\t<img src=\"Images/AntiCulture.gif\" style=\"padding-left:30px;padding-right:30px\" alt=\"Lyrics\">\n\t\t\t\t\t</td>\n\t\t\t\t</tr>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\"SideTop\" align=\"right\">\n\t\t\t\t\t\t<img src=\"Images/SideTop.jpg\" alt=\"Comic Strip\" style=\"display:block\">\t\n\t\t\t\t\t</td>\n\t\t\t\t\t<td class=\"Menu\">\n\t\t\t\t\t\tHome | <a href=\"MusicGenerators.php\">Music Generators</a> | <a href=\"ArtificialSongs.php\">Artificial Songs</a> | <a href=\"http://www.youtube.com/user/AntiCultureDotNet\">Randomly Generated Videos</a> | <a href=\"LyricsGenerator.php\">Lyrics Generator</a> | <a href=\"ComicGenerator.php\">Comic Generator</a> | <a href=\"ImageGenerators.php\">Image Generators</a> | <a href=\"LinguisticTools.php\">Linguistic Tools</a>\t\t\t\t\t</td>\n\t\t\t\t</tr>\n\t\t\t\t<tr>\n\t\t\t\t\t<td class=\"Side\" align=\"left\" valign=\"top\">\n\t\t\t\t\t\t<div style=\"margin-top:15px;margin-left:15px;\"><a href=\"Gallery.php\">Gallery</a><a href=\"Artists.php\">Artists</a><a href=\"Faq.php\">FAQ</a><a href=\"ContactUs.php\">Contact Us</a><a href=\"Donate.php\">Donate</a><a href=\"Links.php\">Links</a></div>\t\t\t\t\t</td>\n\t\t\t\t\t<td class=\"PageContent\" valign=\"top\">\n\t\t\t\t\t\t<div class=\"TopPattern\"></div>\n\t\t\t\t\t\t\r\n\t\t<h1>Random generators</h1>\r\n\t\t<p>\r\n\t\t\tAntiCulture is a museum of Neo Postmodern <a href=\"Faq.php\">meta art</a> where human programming meta <a href=\"Artists.php\">artists</a> create cyber artists such as artificial intelligence beings and random generators. These artificial artistic beings can expose their work of art on the <a href=\"Gallery.php\">gallery</a>. AntiCulture is also a place where interactive art creation and collective art projects take place.\r\n\t\t</p>\r\n\t<h2>Music Generators</h2><p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><a href=\"JazzFusionGenerator\"><img src=\"Images/IconMusic.gif\" alt=\"Jazz Fusion Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"JazzFusionGenerator\">Jazz Fusion Generator</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"HipHopBeatGenerator\"><img src=\"Images/IconMusic.gif\" alt=\"Hip Hop Beat Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"HipHopBeatGenerator\">Hip Hop Beat Generator</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"JazzGenerator.php\"><img src=\"Images/IconMusic.gif\" alt=\"Random Modern Jazz Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"JazzGenerator.php\">Random Modern Jazz Generator</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"RandomTranceMusic.php\"><img src=\"Images/IconMusic.gif\" alt=\"Random Music Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"RandomTranceMusic.php\">Random Trance Music Generator</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"DiatonicComposer.zip\"><img src=\"Images/IconMusic.gif\" alt=\"Classical Music Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"DiatonicComposer.zip\">Classical Music Generator</a> (win32 download)<br><i><b> - Trillian</b></i></td></tr></table></p><h2>Lyrics Generator</h2><p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><a href=\"RandomLyrics.php\"><img src=\"Images/IconLyrics.gif\" alt=\"Random Lyrics Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"RandomLyrics.php\">Random Lyrics Generator</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"RandomFrenchRap.php\"><img src=\"Images/IconLyrics.gif\" alt=\"French Rap Lyrics Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"RandomFrenchRap.php\">French Rap Lyrics Generator</a><br><i><b> - Neotenic</b></i></td></tr></table></p><h2>Comic Strip Generators</h2><p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><a href=\"ComicGenerator.php\"><img src=\"Images/IconComic.gif\" alt=\"Random Comic Strip Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"ComicGenerator.php\">Random Comic Strip Generator</a><br><i><b> - Neotenic</b></i></td></tr></table></p><h2>Image Generators</h2><p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><a href=\"UniverseGenerator\"><img src=\"Images/IconPlanet.gif\" alt=\"Random Universe Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"UniverseGenerator\">Random Universe Generator</a><br><i><b> - Neotenic</b></i></td></tr></table></p><h2>Linguistic Tools</h2><p><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><a href=\"LanguageDetector.php\"><img src=\"Images/IconLinguistics.gif\" alt=\"Language Detector\"></a></td><td style=\"padding-left:12px\"><a href=\"LanguageDetector.php\">Language Detector</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"CompareLanguages.php\"><img src=\"Images/IconLinguistics.gif\" alt=\"Compare Languages\"></a></td><td style=\"padding-left:12px\"><a href=\"CompareLanguages.php\">Compare Languages</a><br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"TextGenerator.php\"><img src=\"Images/IconLinguistics.gif\" alt=\"Text Generator\"></a></td><td style=\"padding-left:12px\"><a href=\"TextGenerator.php\">Text Generator</a> (language oriented)<br><i><b> - Neotenic</b></i></td></tr><tr><td><a href=\"LanguageMutator.php\"><img src=\"Images/IconLinguistics.gif\" alt=\"Language Mutator\"></a></td><td style=\"padding-left:12px\"><a href=\"LanguageMutator.php\">Language Mutator</a><br><i><b> - Neotenic</b></i></td></tr></table></p>\n\n\t\t\t\t\t\t<div class=\"AdSense\" align=\"Center\">\n\t\t\t\t\t\t<script type=\"text/javascript\">\n\t\t\t\t\t\t\t<!--\n\t\t\t\t\t\t\tgoogle_ad_client = \"pub-5136288810208432\";\n\t\t\t\t\t\t\t/* 728x15, created 7/14/08 */\n\t\t\t\t\t\t\tgoogle_ad_slot = \"2447835116\";\n\t\t\t\t\t\t\tgoogle_ad_width = 728;\n\t\t\t\t\t\t\tgoogle_ad_height = 15;\n\t\t\t\t\t\t\t//-->\n\t\t\t\t\t\t</script>\n\t\t\t\t\t\t<script type=\"text/javascript\" src=\"http://pagead2.googlesyndication.com/pagead/show_ads.js\">\n\t\t\t\t\t\t</script>\n\t\t\t\t\t\t\n\t\t\t\t\t\t</div>\n\t\t\t\t\t\t<div align=\"center\" class=\"CopyRight\">\n\t\t\t\t\t\t\t<a href=\"http://www.neotenic.net\">Neotenic Website Design</a> &#169; 2010 - All rights reserved\n\t\t\t\t\t\t</div>\n\t\t\t\t\t</td>\n\t\t\t\t</tr>\n\t\t\t</table>\n\t\t\r\n\r\n</body>\r\n</html>";
            AssertEquals(content.Substring(0, 100), expectedContent.Substring(0, 100));
        }

        private static void AssertEquals(string string1, string string2)
        {
            if (string1 != string2)
                throw new Exception("Wrong value: " + string1 + " should be: " + string2);
        }

        private static void AssertEquals(int int1, int int2)
        {
            if (int1 != int2)
                throw new Exception("Wrong value: " + int1 + " should be: " + int2);
        }
    }
}

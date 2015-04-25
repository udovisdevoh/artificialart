using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Saves and loads lyric song factories
    /// </summary>
    public static class LyricSongFactoryIO
    {
        /// <summary>
        /// Save lyric song factory
        /// </summary>
        /// <param name="iLyricSongFactory">lyric song factory</param>
        /// <param name="xmlFileName">xml file name</param>
        public static void Save(this ILyricSongFactory iLyricSongFactory, string xmlFileName)
        {
            LyricSongFactoryCollection lyricSongFactoryCollection;

            if (iLyricSongFactory is LyricSongFactory)
                lyricSongFactoryCollection = new LyricSongFactoryCollection((LyricSongFactory)iLyricSongFactory);
            else if (iLyricSongFactory is LyricSongFactoryCollection)
                lyricSongFactoryCollection = (LyricSongFactoryCollection)iLyricSongFactory;
            else
                throw new NotImplementedException("Unrecognized lyric song factory type");

            XmlDocument xmlDocument = new XmlDocument();

            try
            {
                xmlDocument.Load(xmlFileName);
            }
            catch (System.IO.FileNotFoundException)
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xmlWriter.WriteStartElement("song");
                xmlWriter.Close();
                xmlDocument.Load(xmlFileName);
            }

            XmlNode root = xmlDocument.DocumentElement;
            XmlElement lyricsSongFactoryCollectionNode;
            XmlNodeList lyricsSongFactoryCollectionNodeList = xmlDocument.GetElementsByTagName("lyricsSongFactoryCollection");

            if (lyricsSongFactoryCollectionNodeList.Count > 0)
            {
                lyricsSongFactoryCollectionNode = (XmlElement)lyricsSongFactoryCollectionNodeList[0];
                lyricsSongFactoryCollectionNode.RemoveAll();
            }
            else
            {
                lyricsSongFactoryCollectionNode = xmlDocument.CreateElement("lyricsSongFactoryCollection");
            }

            lyricsSongFactoryCollectionNode.SetAttribute("LanguageCode", iLyricSongFactory.LanguageCode);

            foreach (LyricSongFactory lyricSongFactory in lyricSongFactoryCollection)
            {
                if (lyricSongFactory.ThemeList.Count == 0 && lyricSongFactory.ThemeBlackList.Count == 0)
                    continue;

                XmlNode lyricSongFactoryNode = xmlDocument.CreateElement("lyricSongFactory");
                XmlElement lyricSongFactoryElement = (XmlElement)lyricSongFactoryNode;

                lyricSongFactoryElement.SetAttribute("BarCount", lyricSongFactory.BarCount.ToString());
                lyricSongFactoryElement.SetAttribute("BarCountPerChorus", lyricSongFactory.BarCountPerChorus.ToString());
                

                XmlNode barInfoNode = xmlDocument.CreateElement("barInfo");
                for (int barId = 0; barId < lyricSongFactory.BarCount; barId++)
                {
                    XmlNode barNode = xmlDocument.CreateElement("bar");
                    XmlElement barElement = (XmlElement)barNode;
                    barElement.SetAttribute("Intensity", lyricSongFactory.GetBarIntensity(barId).ToString(NumberFormatInfo.InvariantInfo));
                    barElement.SetAttribute("LetterCount", lyricSongFactory.GetBarLetterCount(barId).ToString(NumberFormatInfo.InvariantInfo));
                    barInfoNode.AppendChild(barNode);
                }
                lyricSongFactoryNode.AppendChild(barInfoNode);


                XmlNode themeListNode = xmlDocument.CreateElement("themeList");
                foreach (string themeName in lyricSongFactory.ThemeList)
                {
                    XmlNode themeNode = xmlDocument.CreateElement("theme");
                    XmlText textNode = xmlDocument.CreateTextNode(themeName);
                    textNode.Value = themeName;
                    themeNode.AppendChild(textNode);
                    themeListNode.AppendChild(themeNode);
                }
                lyricSongFactoryNode.AppendChild(themeListNode);

                XmlNode themeBlackListNode = xmlDocument.CreateElement("themeBlackList");
                foreach (string themeName in lyricSongFactory.ThemeBlackList)
                {
                    XmlNode themeNode = xmlDocument.CreateElement("theme");
                    XmlText textNode = xmlDocument.CreateTextNode(themeName);
                    textNode.Value = themeName;
                    themeNode.AppendChild(textNode);
                    themeBlackListNode.AppendChild(themeNode);
                }
                lyricSongFactoryNode.AppendChild(themeBlackListNode);

                lyricsSongFactoryCollectionNode.AppendChild(lyricSongFactoryNode);
            }

            root.AppendChild(lyricsSongFactoryCollectionNode);
            xmlDocument.Save(xmlFileName);
        }

        /// <summary>
        /// Load song factory collection from xml file
        /// </summary>
        /// <param name="xmlFileName">xml file name</param>
        /// <param name="lyricSourcePath">lyric source path (folder)</param>
        /// <returns>song factory collection</returns>
        public static LyricSongFactoryCollection Load(string xmlFileName, string lyricSourcePath)
        {
            string languageCode = "en";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileName);

            XmlNodeList lyricsSongFactoryCollectionNodeList = xmlDocument.GetElementsByTagName("lyricsSongFactoryCollection");
            XmlNode lyricsSongFactoryCollectionNode = lyricsSongFactoryCollectionNodeList[0];
            XmlElement lyricsSongFactoryCollectionElement = (XmlElement)lyricsSongFactoryCollectionNode;
            XmlNodeList lyricsSongFactoryNodeList = lyricsSongFactoryCollectionElement.GetElementsByTagName("lyricSongFactory");

            if (lyricsSongFactoryCollectionElement.HasAttribute("LanguageCode"))
                languageCode = lyricsSongFactoryCollectionElement.GetAttribute("LanguageCode");

            string lyricSourceFileName = lyricSourcePath + "lyrics." + languageCode + ".txt";

            LyricSongFactoryCollection lyricSongFactoryCollection = new LyricSongFactoryCollection(lyricSourceFileName, lyricSourcePath, languageCode);

            int lyricSongFactoryIndex = 0;
            foreach (XmlNode lyricSongFactoryNode in lyricsSongFactoryNodeList)
            {
                XmlElement lyricSongFactoryElement = (XmlElement)lyricSongFactoryNode;

                if (lyricSongFactoryCollection.Count <= lyricSongFactoryIndex)
                    lyricSongFactoryCollection.AddNew();

                LyricSongFactory lyricSongFactory = lyricSongFactoryCollection[lyricSongFactoryIndex];

                lyricSongFactory.BarCount = int.Parse(lyricSongFactoryElement.GetAttribute("BarCount"));
                lyricSongFactory.BarCountPerChorus = int.Parse(lyricSongFactoryElement.GetAttribute("BarCountPerChorus"));

                #region Theme List
                XmlNode themeListNode = lyricSongFactoryElement.GetElementsByTagName("themeList")[0];
                XmlElement themeListElement = (XmlElement)themeListNode;
                XmlNodeList themeNodeList = themeListElement.GetElementsByTagName("theme");
                foreach (XmlNode themeNode in themeNodeList)
                    if (themeNode.FirstChild != null)
                        if (themeNode.FirstChild.InnerText != null)
                            lyricSongFactory.AddTheme(themeNode.FirstChild.InnerText); 
                #endregion

                #region Censored Theme List
                XmlNode themeBlackListNode = lyricSongFactoryElement.GetElementsByTagName("themeBlackList")[0];
                XmlElement themeBlackListElement = (XmlElement)themeBlackListNode;
                XmlNodeList themeBlackNodeList = themeBlackListElement.GetElementsByTagName("theme");
                foreach (XmlNode themeNode in themeBlackNodeList)
                    if (themeNode.FirstChild != null)
                        if (themeNode.FirstChild.InnerText != null)
                            lyricSongFactory.CensorTheme(themeNode.FirstChild.InnerText); 
                #endregion

                XmlNode barInfoNode = lyricSongFactoryElement.GetElementsByTagName("barInfo")[0];
                int barIndex = 0;
                foreach (XmlNode barNode in barInfoNode.ChildNodes)
                {
                    XmlElement barElement = (XmlElement)barNode;
                    float intensity = float.Parse(barElement.GetAttribute("Intensity"),NumberFormatInfo.InvariantInfo);
                    short letterCount = short.Parse(barElement.GetAttribute("LetterCount"));
                    lyricSongFactory.SetBarSettings(barIndex, intensity, letterCount);
                    barIndex++;
                }

                lyricSongFactoryIndex++;
            }
            return lyricSongFactoryCollection;
        }
    }
}

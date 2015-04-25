using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace ArtificialArt.Lyrics
{
    /// <summary>
    /// Saves and loads lyric files
    /// </summary>
    public static class LyricIO
    {
        /// <summary>
        /// Save lyrics
        /// </summary>
        /// <param name="lyrics">lyrics</param>
        /// <param name="xmlFileName">xml file name</param>
        public static void Save(this IEnumerable<string> lyrics, string xmlFileName)
        {
            XmlDocument xmlDocument = new XmlDocument();

            if (lyrics == null)
                lyrics = new List<string>();

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
            XmlElement lyricsNode;
            XmlNodeList lyricsNodeList = xmlDocument.GetElementsByTagName("lyrics");

            if (lyricsNodeList.Count > 0)
            {
                lyricsNode = (XmlElement)lyricsNodeList[0];
                lyricsNode.RemoveAll();
            }
            else
            {
                lyricsNode = xmlDocument.CreateElement("lyrics");
            }

            //XmlElement childNode = xmlDocument.CreateElement("childNode");
            //XmlElement childNode2 = xmlDocument.CreateElement("SecondChildNode");
            //XmlText textNode = xmlDocument.CreateTextNode("hello");
            //textNode.Value = "hello, world";
            foreach (string line in lyrics)
            {
                XmlNode lineNode = xmlDocument.CreateElement("line");
                XmlText textNode = xmlDocument.CreateTextNode(line);
                textNode.Value = line;
                lineNode.AppendChild(textNode);
                lyricsNode.AppendChild(lineNode);
            }
            root.AppendChild(lyricsNode);

            //childNode.AppendChild(childNode2);
            //childNode2.SetAttribute("Name", "Value");
            //childNode2.AppendChild(textNode);

            //textNode.Value = "replacing hello world";
            xmlDocument.Save(xmlFileName);
        }

        /// <summary>
        /// Load lyrics
        /// </summary>
        /// <param name="xmlFileName">xml file name</param>
        /// <returns>lyrics</returns>
        public static List<string> Load(string xmlFileName)
        {
            List<string> lyrics = new List<string>();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlFileName);

            XmlNodeList lyricsList = xmlDocument.GetElementsByTagName("lyrics");
            XmlNode lyricsNode = lyricsList[0];

            foreach (XmlNode line in lyricsNode.ChildNodes)
            {
                string lineText = "";
                if (line.FirstChild != null)
                    if (line.FirstChild.Value != null)
                        lineText = line.FirstChild.Value;
                lyrics.Add(lineText);
            }

            return lyrics;
        }
    }
}

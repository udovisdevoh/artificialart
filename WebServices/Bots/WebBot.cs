using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace ArtificialArt.WebServices
{
    /// <summary>
    /// Used to get websites HTML content
    /// </summary>
    public class WebBot
    {
        #region Constants
        /// <summary>
        /// Default user agent
        /// </summary>
        private const string userAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/533.2 (KHTML, like Gecko) Chrome/5.0.342.8 Safari/533.2";
        #endregion

        #region Parts
        /// <summary>
        /// Web client
        /// </summary>
        private WebClient webClient;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a web bot
        /// </summary>
        public WebBot()
        {
            webClient = new WebClient();
            webClient.Headers.Add("user-agent", userAgent);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get page content from URL
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>page content from URL</returns>
        public string GetPageContent(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        /// <summary>
        /// Get page content from URL with post data
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="postData">post data</param>
        /// <returns>page content</returns>
        public string GetPageContent(string url, IDictionary<string, string> postData)
        {
            foreach (string key in new List<string>(postData.Keys))
                postData[key] = HttpUtility.UrlEncode(postData[key]);

            string postString = string.Empty;
            foreach (string key in postData.Keys)
                postString += key + "=" + postData[key] + "&";
            if (postString.EndsWith("&"))
                postString = postString.Substring(0, postString.Length - 1);


            WebRequest webRequest = WebRequest.Create(new Uri(url));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postString.Length;

            using (Stream writeStream = webRequest.GetRequestStream())
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(postString);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.Default))
                    {
                        result = readStream.ReadToEnd();
                    }
                }
            }
            return result;
        }
        #endregion
    }
}

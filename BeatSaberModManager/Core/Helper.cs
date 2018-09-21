using BeatSaberModManager.Dependencies;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BeatSaberModManager.Core
{
    public static class Helper
    {
        private static CookieContainer PermCookie;
        public static string Get(string URL)
        {
            if (PermCookie == null) { PermCookie = new CookieContainer(); }
            HttpWebRequest RQuest = (HttpWebRequest)HttpWebRequest.Create(URL);
            RQuest.Method = "GET";
            RQuest.KeepAlive = true;
            RQuest.CookieContainer = PermCookie;
            RQuest.ContentType = "application/x-www-form-urlencoded";
            RQuest.Referer = "";
            RQuest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
            HttpWebResponse Response = (HttpWebResponse)RQuest.GetResponse();
            StreamReader Sr = new StreamReader(Response.GetResponseStream());
            string Code = Sr.ReadToEnd();
            Sr.Close();
            return Code;
        }
        public static void UnzipFile(byte[] data, string directory)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (var unzip = new Unzip(ms))
                {
                    unzip.ExtractToDirectory(directory);
                }
            }
        }
        public static byte[] GetFile(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            return client.DownloadData(url);
        }
    }
}

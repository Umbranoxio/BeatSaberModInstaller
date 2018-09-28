using BeatSaberModManager.Dependencies;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace BeatSaberModManager.Core
{
    public static class Helper
    {

        public static string Get(string URL) {

            try {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
                request.Method = "GET";
                request.KeepAlive = true;
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader requestReader = new StreamReader(response.GetResponseStream())) {
                    return requestReader.ReadToEnd();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Failed to get version info! Please check your internet connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
                return null;
            }
        }
        public static void UnzipFile(byte[] data, string directory)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (var zf = new ZipFile(ms))
                {
                    foreach (ZipEntry zipEntry in zf)
                    {
                        if (!zipEntry.IsFile)
                            continue;

                        string entryFileName = zipEntry.Name;

                        byte[] buffer = new byte[4096];
                        Stream zipStream = zf.GetInputStream(zipEntry);

                        string fullZipToPath = Path.Combine(directory, entryFileName);
                        string directoryName = Path.GetDirectoryName(fullZipToPath);

                        if (directoryName.Length > 0)
                            Directory.CreateDirectory(directoryName);

                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
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

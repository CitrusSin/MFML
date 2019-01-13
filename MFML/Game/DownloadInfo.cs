using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Game
{
    public class DownloadInfo : ICloneable
    {
        public string id;
        public string path;
        public string url;
        public string sha1;
        public long size;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void DownloadAsync()
        {
            this.DownloadAsync((a, b) => { });
        }

        public void DownloadAsync(AsyncCompletedEventHandler callback)
        {
            this.DownloadAsync(this.path, callback);
        }

        public void DownloadAsync(string path0, AsyncCompletedEventHandler callback)
        {
            string path = path0.Replace('/', '\\');
            if (!Directory.Exists(path.Substring(0, path.LastIndexOf('\\')+1)))
            {
                Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('\\') + 1));
            }
            var wc = new WebClient();
            wc.DownloadFileCompleted += callback;
            wc.DownloadFileCompleted += (a, b) =>
            {
                wc.Dispose();
            };
            wc.DownloadFileAsync(new Uri(url), path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Download
{
    public abstract class Downloader
    {
        public abstract event DownloadProgress OnProgressChanged;
        public virtual string SelectedItem { get; set; }
        public abstract List<string> GetAllItemsToDownload();
        public abstract void Download();
    }
}

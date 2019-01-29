using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Download
{
    public class ForgeDownloadItem : DownloadItem
    {
        public override string Description => throw new NotImplementedException();

        public override void Download()
        {
            throw new NotImplementedException();
        }
    }

    public class ForgeDownloader : Downloader
    {
        public override List<DownloadItem> GetAllItemsToDownload()
        {
            throw new NotImplementedException();
        }
    }
}

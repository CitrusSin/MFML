using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Download
{
    public abstract class DownloadItem
    {
#pragma warning disable CS0067
        public abstract event DownloadProgress OnProgressChanged;
#pragma warning restore CS0067
        public abstract string Description { get; }
        public abstract void Download();

        public override string ToString()
        {
            return this.Description;
        }
    }
}

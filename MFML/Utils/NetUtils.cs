using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Utils
{
    public class NetUtils
    {
        public static bool IsUrlExists(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (Exception)
            {
                return false;
            }
            var request = WebRequest.CreateHttp(uri);
            request.Method = "HEAD";
            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                return response.StatusCode != HttpStatusCode.NotFound;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

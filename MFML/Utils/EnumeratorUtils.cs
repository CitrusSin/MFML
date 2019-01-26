using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFML.Utils
{
    public class EnumeratorUtils
    {
        public static List<T> MakeListFromEnumerator<T>(IEnumerator<T> enumer)
        {
            List<T> list = new List<T>();
            enumer.Reset();
            enumer.MoveNext();
            while (enumer.Current != null)
            {
                list.Add(enumer.Current);
                enumer.MoveNext();
            }
            enumer.Reset();
            return list;
        }
    }
}

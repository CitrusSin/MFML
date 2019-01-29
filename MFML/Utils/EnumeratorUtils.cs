using System;
using System.Collections;
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
            while (enumer.MoveNext())
            {
                list.Add(enumer.Current);
            }
            enumer.Reset();
            return list;
        }

        public static List<object> MakeListFromEnumerator(IEnumerator enumer)
        {
            List<object> list = new List<object>();
            enumer.Reset();
            while (enumer.MoveNext())
            {
                list.Add(enumer.Current);
            }
            enumer.Reset();
            return list;
        }
    }
}

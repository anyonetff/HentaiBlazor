using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class ArrayUtils
    {

        public static bool IsEmpty(object[] a)
        {
            return a == null || a.Length == 0;
        }

        public static bool IsNotEmpty(object[] a)
        {
            return !IsEmpty(a);
        }

    }
}

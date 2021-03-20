using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    // 数组的一些判断
    public class ArrayUtils
    {
        // 数组是否为空
        public static bool IsEmpty(object[] a)
        {
            return a == null || a.Length == 0;
        }

        // 数组是否不为空
        public static bool IsNotEmpty(object[] a)
        {
            return !IsEmpty(a);
        }

    }
}

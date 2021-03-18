using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public class StringUtils
    {

        public static bool IsBlank(string s)
        {
            return s == null || s.Length == 0 || s.Trim().Length == 0;
        }

        public static bool IsNotBlank(string s)
        {
            return !IsBlank(s);
        }

        public static bool IsEmpty(string s)
        {
            return s == null || s.Length == 0;
        }

        public static bool IsNotEmpty(string s)
        {
            return !IsEmpty(s);
        }

    }
}

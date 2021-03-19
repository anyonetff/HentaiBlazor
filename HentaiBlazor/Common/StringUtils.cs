using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
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

        public static bool IsAnyContains(string s, string[] a, StringComparison stringComparison) 
        {
            if (IsEmpty(s) || ArrayUtils.IsEmpty(a)) 
            {
                return false;
            }

            foreach (var t in a)
            {
                if (IsNotEmpty(t) && s.Contains(t, stringComparison))
                {
                    return true;
                }
            }

            return false;
        }

    }
}

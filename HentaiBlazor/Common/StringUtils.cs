using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class StringUtils
    {

        public static bool IsNotEqual(string s, string o)
        {
            return ! IsEqual(s, o);
        }

        public static bool IsEqual(string s, string o)
        {
            return IsEqual(s, o, StringComparison.Ordinal);
        }

        public static bool IsNotEqualIgnoreCase(string s, string o)
        {
            return ! IsEqualIgnoreCase(s, o);
        }

        public static bool IsEqualIgnoreCase(string s, string o)
        {
            return IsEqual(s, o, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsEqual(string s, string o, StringComparison stringComparison)
        {
            if (s == null && o == null)
            {
                return true;
            }

            if (s != null && o != null)
            {
                return s.Equals(o, stringComparison);
            }

            return false;
        }

        public static bool IsNotEqual(string s, string o, StringComparison stringComparison) 
        {
            return !IsEqual(s, o, stringComparison);
        }

        // 字符串是否为空，空字符也算空
        public static bool IsBlank(string s)
        {
            return s == null || s.Length == 0 || s.Trim().Length == 0;
        }

        // 字符串是否不为空，空字符也算空
        public static bool IsNotBlank(string s)
        {
            return !IsBlank(s);
        }

        // 字符串是否为空
        public static bool IsEmpty(string s)
        {
            return s == null || s.Length == 0;
        }

        // 字符串是否不为空
        public static bool IsNotEmpty(string s)
        {
            return !IsEmpty(s);
        }

        // 字符是否包含一组值中的所有
        public static bool IsAllContains(string s, string[] a, StringComparison stringComparison)
        {
            // 如果用于对比的元素为空
            if (IsEmpty(s) || ArrayUtils.IsEmpty(a))
            {
                return false;
            }

            // 循环比较每个元素
            foreach (var t in a)
            {
                // 如果被比较的字符串不为空，并且不包含
                if (IsNotEmpty(t) && ! s.Contains(t, stringComparison))
                {
                    return false;
                }
            }

            return true;
        }

        // 字符是否包含一组值中的一个
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

        // 字符是否不不包含任何一个
        public static bool IsNoneContains(string s, string[] a, StringComparison stringComparison)
        {
            // 如果用于对比的元素为空
            if (IsEmpty(s) || ArrayUtils.IsEmpty(a))
            {
                return false;
            }

            // 循环比较每个元素
            foreach (var t in a)
            {
                // 如果被比较的字符串不为空，并且不包含
                if (IsNotEmpty(t) && s.Contains(t, stringComparison))
                {
                    return false;
                }
            }

            return true;
        }

    }
}

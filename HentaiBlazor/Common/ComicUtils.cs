using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class ComicUtils
    {

        // 获取作者
        public static string ParseAuthor(string name)
        {
            string r = "";

            int s = name.IndexOf("[");
            int e = name.IndexOf("]");

            if (s > -1 && e > 1)
            {
                r = name.Substring(s + 1, e - s - 1).Trim();
            }

            Console.WriteLine("   [ " + s + " - " + e + " ] " + r);

            return r;
        }

        // 获取标题
        public static string ParseTitle(string name)
        {
            string r = "";

            int s = name.IndexOf("]");
            int e = name.LastIndexOf(".");

            if (s > -1 && e > 1)
            {
                r = name.Substring(s + 1, e - s - 1).Trim();
            }

            Console.WriteLine("   [ " + s + " - " + e + " ] " + r);

            return r;
        }

        public static string ParseLanguage(string name)
        {
            if (IsZh(name))
            {
                return "zh";
            }

            if (IsEn(name))
            {
                return "en";
            }

            return "ja";
        }

        private static string[] _Zh = { "中国", "中文", "汉化", "漢化" };

        private static string[] _En = { "English" };

        public static bool IsZh(string name)
        {
            return StringUtils.IsAnyContains(name, _Zh, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsEn(string name)
        {
            return StringUtils.IsAnyContains(name, _En, StringComparison.OrdinalIgnoreCase);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class AnimeUtils
    {
        private static string[] _media = { ".mp4", ".wma", ".avi", ".mkv", ".rmvb", ".rm" };

        public static bool IsMedia(string name)
        {
            if (StringUtils.IsBlank(name))
            {
                return false;
            }

            foreach (var media in _media)
            {
                if (name.EndsWith(media, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static string ParseTitle(string name)
        {
            string r = "";

            int s = name.IndexOf("]");
            int e = name.LastIndexOf(".");

            if (s > -1 && e > 1)
            {
                r = name.Substring(s + 1, e - s - 1).Trim();
            }
            else
            {
                r = name.Substring(0, e).Trim();
            }

            // Console.WriteLine("   [ " + s + " - " + e + " ] " + r);

            return r;
        }


    }
}

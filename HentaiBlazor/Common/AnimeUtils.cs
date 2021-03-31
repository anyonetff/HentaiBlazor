using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Common
{
    public class AnimeUtils
    {
        private static string[] _media = { ".mp4", ".wma", ".avi" };

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

    }
}

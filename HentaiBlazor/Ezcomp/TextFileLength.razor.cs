using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class TextFileLength
    {
        private static long KB = 1024L;

        private static long MB = 1024L * 1024L;

        private static long GB = 1020L * 1024L * 1024L;

        private static long TB = 1020L * 1024L * 1024L * 1024L;

        [Parameter]
        public long FileLength { get; set; }

        private string _text()
        {
            if (FileLength < KB) 
            {
                return FileLength + " byte";
            }
            if (FileLength < MB)
            {
                return FileLength / KB + " KB";
            }
            if (FileLength < GB)
            {
                return FileLength / MB + " MB";
            }
            if (FileLength < TB)
            {
                return FileLength / GB + " GB";
            }

            return FileLength / TB + " TB";
        }

    }
}

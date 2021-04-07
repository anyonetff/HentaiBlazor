using AntDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public class SortableItem
    {
        
        public int Index { get; set; }

        public int Mode { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public static SortableItem Of(int mode, string name)
        {
            return new SortableItem { Mode = mode, Name = name };
        }

        public static SortableItem Of(int mode, string name, string text)
        {
            return new SortableItem { Mode = mode, Name = name, Text = text };
        }

        public static SortableItem Asc(string name)
        {
            return Of(1, name);
        }

        public static SortableItem Desc(string name)
        {
            return Of(-1, name);
        }

        public string _Type()
        {
            return Mode == 0 ? ButtonType.Default : ButtonType.Primary;
        }

        public string _Icon()
        {
            if (Mode == 0)
                return "";

            return (Mode > 0) ? "caret-up" : "caret-down";
        }

    }
}

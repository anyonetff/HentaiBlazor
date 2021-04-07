using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public class Sortable
    {

        public List<SortableItem> SortableItems { get; set; }

        private static readonly int _None = 0;

        private static readonly int _Asc = 1;

        private static readonly int _Desc = - 1;

        public Sortable()
        {
            SortableItems = new List<SortableItem>();
        }

        public void AddAsc(string name, string text) 
        {
            Add(SortableItem.Of(_Asc, name, text));
        }

        public void AddDesc(string name, string text)
        {
            Add(SortableItem.Of(_Desc, name, text));
        }

        public void Add(string name, string text)
        {
            Add(SortableItem.Of(_None, name, text));
        }

        private void Add(SortableItem item)
        {
            if (Contains(item))
            {
                return;
            }

            if (item.Mode != _None)
            {
                item.Index = Count() + 1;
            }

            SortableItems.Add(item);
        }

        public bool Contains(SortableItem item)
        {
            return SortableItems.Where(i => i.Name == item.Name).Any();
        }

        public int Count()
        {
            return SortableItems.Where(i => i.Mode != _None).Count();
        }

        public void Clear()
        {
            foreach (var item in SortableItems)
            {
                item.Index = _None;
                item.Mode = _None;
            }
        }

        public void Order(string name)
        {
            foreach (var item in SortableItems)
            {
                if (name == item.Name)
                {
                    if (item.Index == 0)
                    {
                        item.Index = Count() + 1;
                        item.Mode = _Asc;
                    }
                    else
                    {
                        item.Mode = -1 * item.Mode;
                    }
                }
            }
        }

        public List<SortableItem> ToOrders()
        {
            return SortableItems
                .Where(i => i.Mode != _None)
                .OrderBy(i => i.Index).ToList();
        }

    }
}

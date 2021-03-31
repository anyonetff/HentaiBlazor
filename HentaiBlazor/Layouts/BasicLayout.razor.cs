using AntDesign.Pro.Layout;
using HentaiBlazor.Common;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Service.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor
{
    public partial class BasicLayout
    {
        private MenuDataItem[] Menus;

        private int _maxDepth = 3;

        [Inject]
        public FunctionService service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Menus = await CreateMenu(null, 1);
        }

        private async Task<MenuDataItem[]> CreateMenu(MenuDataItem parent, int depth)
        {
            string _parent = (parent == null) ? "0" : parent.Key;

            List<MenuDataItem> items = new List<MenuDataItem>();

            List<SFunctionEntity> roots = await service.ListByParentAsync(_parent);

            foreach (var r in roots)
            {
                MenuDataItem mi = CreateMenuItem(r);

                // 当前节点不是叶子节点，并且小于最大递归深度
                if (!r.Leaf && depth < _maxDepth)
                {
                    MenuDataItem[] children = await CreateMenu(mi, depth + 1);

                    if (children.Any())
                    {
                        mi.Children = children;
                    }
                }

                items.Add(mi);
            }

            return items.ToArray();
        }

        private MenuDataItem CreateMenuItem(SFunctionEntity f)
        {
            MenuDataItem m = new MenuDataItem();

            m.Key = f.Id;
            m.Name = f.Name;
            m.Path = f.Path;
            m.Icon = f.Icon;

            return m;
        }


    }
}

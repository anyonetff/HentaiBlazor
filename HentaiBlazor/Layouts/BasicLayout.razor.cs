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

        [Inject]
        public FunctionService service { get; set; }

        protected override async Task OnInitializedAsync()
        {
            List<SFunctionEntity> roots = await service.ListByParentAsync("0");

            List<MenuDataItem> data = new List<MenuDataItem>();

            foreach (var r in roots) 
            {
                MenuDataItem m = Create(r);

                if (! r.Leaf)
                {
                    List<MenuDataItem> data2 = new List<MenuDataItem>();

                    List<SFunctionEntity> children = await service.ListByParentAsync(r.Id);

                    foreach (var c in children)
                    {
                        MenuDataItem n = Create(c);

                        data2.Add(n);
                    }

                    if (data2.Any())
                    {
                        m.Children = data2.ToArray();
                    }
                }

                data.Add(m);
            }

            Menus = data.ToArray();
        }

        private MenuDataItem Create(SFunctionEntity f)
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

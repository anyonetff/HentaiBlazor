using AntDesign.Pro.Layout;
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
            List<SFunctionEntity> results = await service.ListAsync();

            List<MenuDataItem> data = new List<MenuDataItem>();

            foreach (var r in results) 
            {
                MenuDataItem m = new MenuDataItem();

                m.Key = r.Id;
                m.Name = r.Name;
                m.Path = r.Path;
                m.Icon = r.Icon;

                data.Add(m);
            }

            Menus = data.ToArray();
        }
    }
}

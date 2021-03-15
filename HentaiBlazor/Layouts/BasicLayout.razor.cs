using AntDesign.Pro.Layout;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor
{
    public partial class BasicLayout
    {
        private MenuDataItem[] _menuData =
{
        new MenuDataItem
        {
            Path = "/",
            Name = "welcome",
            Key = "welcome",
            Icon = "smile",
        },
        new MenuDataItem
        {
            Path = "/basic/catalog/list",
            Name = "目录管理",
            Key = "catalog",
            Icon = "smile",
        },
        new MenuDataItem
        {
            Path = "/basic/author/list",
            Name = "作者管理",
            Key = "author",
            Icon = "smile",
        }
    };
    }
}

using AntDesign;
using HentaiBlazor.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    // 封装了一个简单的排序按钮
    // 这里简单演示了带事件的按钮是如何绑定的
    public partial class ButtonSortable
    {

        [Parameter]
        public Sortable Sortable { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }


        private string _Type()
        {
            return Name == Sortable.Name ? ButtonType.Primary : ButtonType.Default;
        }

        private string _Icon()
        {
            if (Name != Sortable.Name)
                return "";

            return (Sortable.Mode > 0) ? "caret-up" : "caret-down";
        }

        // 这个是从Ant组件库里抄来的写法
        // 为何要这么写，我也不知道...
        private async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }


    }
}

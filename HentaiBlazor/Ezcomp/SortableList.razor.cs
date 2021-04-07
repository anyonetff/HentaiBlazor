using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class SortableList
    {

        [Parameter]
        public Sortable Sortable { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnSort { get; set; }

        private async Task HandleOnSort(MouseEventArgs args)
        {
            if (OnSort.HasDelegate)
            {
                await OnSort.InvokeAsync(args);
            }
        }

        private async Task OnClear(MouseEventArgs args)
        {
            Sortable.Clear();

            await HandleOnSort(args);
        }

        private async Task OnChange(MouseEventArgs args, string name)
        {
            Sortable.Order(name);

            await HandleOnSort(args);
        }

    }
}

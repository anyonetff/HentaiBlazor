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
        public IEnumerable<Sortable> Sortables { get; set; }


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
            if (Sortables == null || !Sortables.Any())
            {
                return;
            }

            foreach (var item in Sortables)
            {
                item.Mode = 0;
            }

            await HandleOnSort(args);
        }

        private async Task OnClick(MouseEventArgs args, Sortable item)
        {
            if (item.Mode == 0)
            {
                item.Mode = 1;
            }
            else
            {
                item.Mode = item.Mode * -1;
            }

            await HandleOnSort(args);
        }

    }
}

using HentaiBlazor.Data.Comic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class CardBook
    {
        [Parameter]
        public CBookEntity Book { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnDetail { get; set; }

        private async Task HandleOnDetail(MouseEventArgs args)
        {
            if (OnDetail.HasDelegate)
            {
                await OnDetail.InvokeAsync(args);
            }
        }
    }
}

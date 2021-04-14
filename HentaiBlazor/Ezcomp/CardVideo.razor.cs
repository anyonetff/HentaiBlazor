using HentaiBlazor.Data.Anime;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class CardVideo
    {
        [Parameter]
        public AVideoEntity Video { get; set; }

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

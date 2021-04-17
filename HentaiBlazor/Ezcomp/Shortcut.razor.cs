using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Ezcomp
{
    public partial class Shortcut
    {
        private static readonly string[] _index = {
            "*", "0-9",
            "A", "B", "C", "D", "E", "F", "G",
            "H", "I", "J", "K", "L", "M", "N",
            "O", "P", "Q", "R", "S", "T",
            "U", "V", "W", "X", "Y", "Z",
            "?"
        };

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public EventCallback<string> OnChange { get; set; }


        private async Task HandleOnChange(string value)
        {
            
            Value = value;

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(value);
            }
        }


    }

    

}

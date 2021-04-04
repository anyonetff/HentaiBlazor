using AntDesign;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Services.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security.Function
{
    public partial class Remove
    {
        private string functionId;

        private SFunctionEntity functionEntity;

        [Inject]
        public FunctionService functionService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            functionId = base.Options;

            functionEntity = await functionService.FindAsync(functionId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除标签[" + functionEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await functionService.RemoveAsync(functionEntity);

            await base.OkAsync(args);
        }
    }
}

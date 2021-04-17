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

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除功能[" + functionEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await functionService.RemoveAsync(functionEntity);

            await base.OnFeedbackOkAsync(args);
        }
    }
}

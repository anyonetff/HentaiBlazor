using AntDesign;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Services.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security.User
{
    public partial class Remove
    {
        private string userId;

        private SUserEntity userEntity;

        [Inject]
        public UserService userService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            userId = base.Options;

            userEntity = await userService.FindAsync(userId);

            await base.OnInitializedAsync();
        }

        public override async Task OnFeedbackOkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除用户[" + userEntity.Id + "]");

            if (FeedbackRef is ConfirmRef confirmRef)
            {
                confirmRef.Config.OkButtonProps.Loading = true;
                await confirmRef.UpdateConfigAsync();
            }

            await userService.RemoveAsync(userEntity);

            await base.OnFeedbackOkAsync(args);
        }
    }
}

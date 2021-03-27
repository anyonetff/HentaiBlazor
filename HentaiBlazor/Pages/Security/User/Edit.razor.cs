using HentaiBlazor.Common;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Service.Security;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security.User
{
    public partial class Edit
    {

        private ValidationMessageStore messageStore;

        // private int _times = 0;

        private string userId;

        private SUserEntity userEntity;

        [Inject]
        public UserService userService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            userId = base.Options ?? null;

            if (StringUtils.IsBlank(userId))
            {
                userEntity = new SUserEntity();
            }
            else 
            {
                userEntity = await userService.FindCloneAsync(userId);
            }

            await base.OnInitializedAsync();
        }

        private async Task ValidatorByUsername(EditContext editContext)
        {
            // 强制设置为小写
            userEntity.Username = userEntity.Username.Trim().ToLower();

            SUserEntity other = await this.userService.FindByUsernameAsync(userEntity.Username);

            if (other != null && StringUtils.IsNotEqual(other.Id, userEntity.Id))
            {
                messageStore.Add(editContext.Field(nameof(SUserEntity.Username)), "用户名不合法");
            }
        }

        private async Task ValidatorByPassword(EditContext editContext)
        {
            if (StringUtils.IsNotEqual(userEntity.PasswordNew, userEntity.PasswordConfirm))
            {
                messageStore.Add(editContext.Field(nameof(SUserEntity.PasswordConfirm)), "确认密码不相同");
            }

            await Task.CompletedTask;
        }

        private async Task OnFinish(EditContext editContext)
        {
            Console.WriteLine("开始提交表单[" + userEntity + "].");

            messageStore = new ValidationMessageStore(editContext);

            await this.ValidatorByUsername(editContext);
            await this.ValidatorByPassword(editContext);

            if (editContext.GetValidationMessages().Any())
            {
                editContext.NotifyValidationStateChanged();
                messageStore.Clear();

                return;
            }

            userEntity.Password = DigestUtils.Md5Hex(userEntity.PasswordNew);

            await this.userService.SaveAsync(userEntity);

            await base.ModalRef.CloseAsync();
        }

        private async Task OnFinishFailed(EditContext editContext)
        {
            await Task.CompletedTask;
        }

    }
}

using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Tag
{
    public partial class Remove
    {
        private string tagId;

        private BTagEntity tagEntity;

        [Inject]
        public TagService tagService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tagId = base.Options;

            tagEntity = await tagService.FindAsync(tagId);

            await base.OnInitializedAsync();
        }

        public override async Task OkAsync(ModalClosingEventArgs args)
        {
            Console.WriteLine("删除标签[" + tagEntity.Id + "]");

            ConfirmRef.Config.OkButtonProps.Loading = true;

            await tagService.RemoveAsync(tagEntity);

            await base.OkAsync(args);
        }
    }
}

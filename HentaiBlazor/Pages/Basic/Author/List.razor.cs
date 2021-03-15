using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Author
{
    public partial class List
    {
        [Inject]
        public AuthorService service { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        List<BAuthorEntity> BAuthorEntities;
        protected override async Task OnInitializedAsync()
        {
            BAuthorEntities = await service.ListAsync();
        }

        Func<ModalClosingEventArgs, Task> onOk = (e) =>
        {
            Console.WriteLine("Ok");
            return Task.CompletedTask;
        };

        Func<ModalClosingEventArgs, Task> onCancel = (e) =>
        {
            Console.WriteLine("Cancel");
            return Task.CompletedTask;
        };

        private void DeleteConfirm(String Id)
        {
            _modal.Confirm(new ConfirmOptions()
            {
                Title = "Do you Want to delete these items?",
               // Icon = icon,
                Content = Id,
                OnOk = onOk,
                OnCancel = onCancel
            });
        }

    }
}

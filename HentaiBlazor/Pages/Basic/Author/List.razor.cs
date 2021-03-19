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
        public AuthorService authorService { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        private ModalRef _editRef;

        private List<BAuthorEntity> BAuthorEntities;

        private string searchKeyword;

        protected override async Task OnInitializedAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            Console.WriteLine(" search: " + searchKeyword);

            BAuthorEntities = await authorService.SearchAsync(searchKeyword);
        }

        private async Task OpenModify(string id)
        {
            var templateOptions = await authorService.FindAsync(id);

            await this.OpenEdit(templateOptions);
        }

        private async Task OpenEdit(BAuthorEntity templateOptions)
        {
            var modalConfig = new ModalOptions();

            modalConfig.Title = "编辑数据";
            modalConfig.Footer = null;

            _editRef = await _modal
                .CreateModalAsync<Edit, BAuthorEntity>(modalConfig, templateOptions);
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

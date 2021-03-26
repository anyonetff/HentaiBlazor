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

        private ConfirmRef _removeRef;

        private List<BAuthorEntity> BAuthorEntities;

        private string searchMode = "MASTER";

        private string searchKeyword = "";


        protected override async Task OnInitializedAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            BAuthorEntities = await authorService.SearchAsync(searchMode, searchKeyword);
        }

        private async Task OpenEdit(string options)
        {
            var _config = new ModalOptions();

            _config.Title = "编辑数据";
            _config.Footer = null;

            _config.AfterClose = async () =>
            {
                Console.WriteLine("关闭编辑弹窗后刷新数据...");

                await Search();
                StateHasChanged();
            };

            _editRef = await _modal
                .CreateModalAsync<Edit, string>(_config, options);
        }

        private async Task OpenRemove(string options)
        {
            var _config = new ConfirmOptions();

            _config.Title = "删除数据";

            _config.OnOk = async (r) =>
            {
                Console.WriteLine("关闭删除确认后刷新数据...");

                await Search();
                StateHasChanged();
            };

            _removeRef = await _modal.CreateConfirmAsync<Remove, string, string>(_config, options);
        }

    }
}

using HentaiBlazor.Common;
using HentaiBlazor.Constant;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Scan
    {

        private bool _scaning = false;

        private int _total = 0;

        private int _current = 0;

        private int _percent = 0;

        private string catalogId;

        private BCatalogEntity catalogEntity;

        [Inject]
        public CatalogService catalogService { get; set; }

        [Inject]
        public AuthorService authorService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //catalog = new BCatalogEntity();
            catalogId = base.Options;

            catalogEntity = await catalogService.FindAsync(catalogId);

            await base.OnInitializedAsync();
        }

        private async Task Stop()
        {
            if (_scaning)
            {
                Console.WriteLine("中止操作...");

                _scaning = false;
            }

            await Task.CompletedTask;
        }

        private async Task Refresh()
        {
            Console.WriteLine("重置上次扫描时间...");

            catalogEntity.Refresh = DateTime.MinValue;

            catalogEntity = await this.catalogService.SaveAsync(catalogEntity);
        }

        private async Task Start()
        {
            if (_scaning)
            {
                return;
            }

            _scaning = true;
            //_percent = 50;

            //catalogEntity.Refresh = DateTime.Now;

            await InvokeAsync(StateHasChanged);

            DateTime _refresh = DateTime.Now;
            int _items = 0;

            switch (catalogEntity.Usage)
            {
                case nameof(UsageConstant.COMIC):
                    Console.WriteLine("漫画目录:");
                    _items = await this.DiscoveryComic(
                        catalogEntity.Path, catalogEntity.Children, catalogEntity.Refresh);
                    break;

                case nameof(UsageConstant.ANIME):
                    Console.WriteLine("动画目录:");
                    await this.DiscoveryAnime(catalogEntity.Path, catalogEntity.Children);
                    break;

                default:
                    Console.WriteLine("所选目录用途不明");
                    break;
            }

            catalogEntity.Refresh = _refresh;
            catalogEntity.Items = _items;

            catalogEntity = await this.catalogService.SaveAsync(catalogEntity);

            Console.WriteLine("完成目录扫描...");

            _percent = 100;

            _scaning = false;
        }


        private async Task<BAuthorEntity> CreateAuthor(string name)
        {
            if (name == null || name == "")
            {
                return null;
            }

            BAuthorEntity author = await authorService.FindByNameAsync(name);

            if (author != null)
            {
                return author;
            }

            author = new BAuthorEntity();

            //author.Id = Guid.NewGuid().ToString();
            author.Name = name;
            author.Alias = ".";
            author.Valid = true;

            author = await authorService.SaveAsync(author);

            return author;
        }

    }
}

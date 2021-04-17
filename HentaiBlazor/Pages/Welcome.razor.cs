using HentaiBlazor.Services.Anime;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages
{
    public partial class Welcome
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public BookService bookService { get; set; }

        [Inject]
        public VideoService videoService { get; set; }

        [Inject]
        public AuthorService authorService { get; set; }

        [Inject]
        public TagService tagService { get; set; }

        private int BookTotal;

        private int VideoTotal;

        private int AuthorTotal;

        private int TagTotal;

        private string shortcut;

        protected override async Task OnInitializedAsync()
        {
            BookTotal = await bookService.TotalCountAsync();
            VideoTotal = await videoService.TotalCountAsync();
            AuthorTotal = await authorService.TotalCountAsync();
            TagTotal = await tagService.TotalCountAsync();
        }

        public void navTo(string url)
        {
            NavigationManager.NavigateTo(url);
        }

        private void OnValueChanged(string value)
        {
            shortcut = value;

            Console.WriteLine(shortcut);
        }

    }
}

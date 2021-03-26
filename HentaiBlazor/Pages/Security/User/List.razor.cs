using AntDesign;
using HentaiBlazor.Data.Security;
using HentaiBlazor.Service.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security.User
{
    public partial class List
    {
        [Inject]
        public UserService userService { get; set; }

        [Inject]
        public ModalService _modal { get; set; }

        private ModalRef _editRef;

        private ConfirmRef _removeRef;

        private List<SUserEntity> SUserEntities;

        private string searchKeyword;

        protected override async Task OnInitializedAsync()
        {
            await Search();
        }

        public async Task Search()
        {
            SUserEntities = await userService.SearchAsync(searchKeyword);
        }
    }
}

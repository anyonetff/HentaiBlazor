using HentaiBlazor.Data.Security;
using HentaiBlazor.Service.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Security
{
    public partial class Profile
    {

        private SUserEntity userEntity;

        [Inject]
        public UserService userService { get; set; }

    }
}

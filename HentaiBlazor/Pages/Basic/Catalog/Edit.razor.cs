using AntDesign;
using HentaiBlazor.Data.Basic;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Pages.Basic.Catalog
{
    public partial class Edit
    {

        private BCatalogEntity catalog;

        protected override void OnInitialized()
        {
            catalog = new BCatalogEntity();
            //_model = base.Options ?? new Form.demo.Basic.Model();
            base.OnInitialized();
        }

        private void OnFinish(EditContext editContext)
        {
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(_model)}");
            
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}

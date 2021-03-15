using AntDesign;
using HentaiBlazor.Data.Basic;
using HentaiBlazor.Service.Basic;
using Microsoft.AspNetCore.Components;
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

        [Inject]
        public CatalogService service { get; set; }

        protected override void OnInitialized()
        {
            //catalog = new BCatalogEntity();
            catalog = base.Options ?? new BCatalogEntity();
            base.OnInitialized();
        }

        private async Task OnFinish(EditContext editContext)
        {
            //Console.WriteLine($"Success:{JsonSerializer.Serialize(_model)}");
            

            if (catalog.Id == null || catalog.Id == "")
            {
                Console.WriteLine("添加一条记录");
                catalog.Id = Guid.NewGuid().ToString();
                await service.AddAsync(catalog);
            }
            else
            {
                Console.WriteLine("修改一条记录");
                service.Update(catalog);
            }

            _ = base.ModalRef.CloseAsync();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            //Console.WriteLine($"Failed:{JsonSerializer.Serialize(_model)}");
        }
    }
}

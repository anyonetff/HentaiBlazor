using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AntDesign.Pro.Layout;
using HentaiBlazor.Data;
using Microsoft.EntityFrameworkCore;
using HentaiBlazor.Services.Basic;
using HentaiBlazor.Services.Comic;
using HentaiBlazor.Services.Security;
using HentaiBlazor.Services.Anime;
using HentaiBlazor.Services;

namespace HentaiBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddAntDesign();
			services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
            });
            services.Configure<ProSettings>(Configuration.GetSection("ProSettings"));

            services.AddDbContextFactory<HentaiContext>(options =>
            {
                Console.WriteLine("创建数据源工厂:");
                options.UseSqlite($"Data Source ={HentaiContext.HentaiDb}.db");
            });

            services.AddScoped<FunctionService>();
            services.AddScoped<UserService>();

            services.AddScoped<AuthorService>();
            services.AddScoped<ProducerService>();
            services.AddScoped<TagService>();
            services.AddScoped<CatalogService>();

            services.AddScoped<BookService>();
            services.AddScoped<BookTagService>();
            services.AddScoped<VideoService>();
            services.AddScoped<VideoTagService>();

            services.AddSingleton<CoverService>();
            services.AddSingleton<PreviewService>();

            //services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                endpoints.MapControllers();
            });
        }
    }
}

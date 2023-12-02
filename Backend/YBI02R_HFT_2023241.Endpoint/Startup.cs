using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Logic;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;
using YBI02R_HFT_2023241.Repository.Repositories.ModelRepos;
using YBI02R_HFT_2023241.Logic.Classes;

namespace YBI02R_HFT_2023241.Endpoint
{
    public class Startup
    {

        public Startup()
        {
               
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MusicDbContext>();
            services.AddTransient<IRepository<Song>, SongRepo>();
            services.AddTransient<IRepository<Artist>, ArtistRepo>();
            services.AddTransient<IRepository<Publisher>, PublisherRepo>();
            services.AddTransient<ISongLogic, SongLogic>();
            services.AddTransient<IArtistLogic, ArtistLogic>();
            services.AddTransient<IPublisherLogic, PublisherLogic>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}

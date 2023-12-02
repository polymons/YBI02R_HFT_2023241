using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YBI02R_HFT_2023241.Logic.Interfaces;
using YBI02R_HFT_2023241.Models;
using YBI02R_HFT_2023241.Repository.Database;
using YBI02R_HFT_2023241.Repository.Interfaces;
using YBI02R_HFT_2023241.Repository.Repositories.ModelRepos;
using YBI02R_HFT_2023241.Logic.Classes;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;

namespace YBI02R_HFT_2023241.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            services.AddControllers();
            services.AddSwaggerGen(swagger => swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "YBI02R_HFT_2023241.Endpoint", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "YBI02R_HFT_2023241.Endpoint v1"));
            }

            app.UseExceptionHandler(handler => handler.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

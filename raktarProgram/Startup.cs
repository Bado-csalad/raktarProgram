using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using raktarProgram.Data;
using raktarProgram.Interfaces;
using raktarProgram.Repositories;
using Microsoft.EntityFrameworkCore;
using Blazorise;
using raktarProgram.Services;
using Blazorise.Material;
using Blazorise.Icons.Material;

namespace raktarProgram
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
            services
                .AddBlazorise(options =>
               {
                   options.ChangeTextOnKeyPress = true; // optional
                })
                .AddMaterialProviders()
                .AddMaterialIcons();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<SearchService>();
            services.AddDbContext<RaktarContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IEszkozRepository, EszkozRepository>();
            services.AddTransient<IEszkozHelyRepository, EszkozHelyRepository>();
            services.AddTransient<IEszkozHelyTipusRepository, EszkozHelyTipusRepository>();
            services.AddTransient<IHomeRespitory, HomeRespitory>();
            services.AddTransient<IEszkozTipusRepository, EszkozTipusRepository>();
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
                .UseMaterialProviders()
                .UseMaterialIcons();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}

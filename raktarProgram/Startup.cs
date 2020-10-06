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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using raktarProgram.Areas.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

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
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        IConfigurationSection googleAuthNSection =
            //            Configuration.GetSection("Authentication:Google");

            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    })
            //    .AddMicrosoftAccount(microsoftOptions =>
            //    {
            //        microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
            //        microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            //    })
            //    .AddFacebook(options =>
            //    {
            //        options.AppId = Configuration["Authentication:Facebook:AppId"];
            //        options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //        options.AccessDeniedPath = "/AccessDeniedPathInfo";
            //    });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FelhasznaloConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddTransient<IEmailSender, EmailSender>();


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

            app.UseAuthentication();
            app.UseAuthorization();

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

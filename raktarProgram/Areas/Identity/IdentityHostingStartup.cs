using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using raktarProgram.Repositories;

[assembly: HostingStartup(typeof(raktarProgram.Areas.Identity.IdentityHostingStartup))]
namespace raktarProgram.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { 
            });

        }

    }
}

/*Roles:
    1. Owner => csak en, alapból benne az adatbazisban
    2. Admin => tud jogokat adni, és az alatta lévő jogokat megkapja
    3. Leader => tud beszeren/atadni/törölni/modosítani
    4. Visitor => csak megnézni tud
 */
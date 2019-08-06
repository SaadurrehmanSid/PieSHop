using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Data_Access_Layer;
using PieShop.Models;

[assembly: HostingStartup(typeof(PieShop.Areas.Identity.IdentityHostingStartup))]
namespace PieShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
         
            builder.ConfigureServices((context, services) => {
                
                services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                 .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppDbContext>();
            });

          

        }
    }
}
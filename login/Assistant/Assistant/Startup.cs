﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assistant.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebEssentials.AspNetCore.Pwa;
using Microsoft.AspNetCore.Routing;
using Assistant.Models;
using Assistant.Areas.Identity.Pages.Account.Manage;
using AspNetCore.Identity.MongoDB;

namespace Assistant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            //services.AddSingleton<MongoDbContext>();

            //services.AddIdentityMongoDbProvider<ApplicationUser>();

            services.AddIdentity<ApplicationUser, MongoIdentityRole>()
                .AddDefaultTokenProviders();
            services
                .Configure<MongoDBOption>(Configuration.GetSection("MongoDBOption"))
                .AddMongoDatabase()
                .AddMongoDbContext<ApplicationUser, MongoIdentityRole>()
                .AddMongoStore<ApplicationUser, MongoIdentityRole>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddProgressiveWebApp(
            new PwaOptions
            {
                RegisterServiceWorker = true,
                RegisterWebmanifest = false,
                Strategy = ServiceWorkerStrategy.NetworkFirst,
                OfflineRoute = "/SavedOfflineList.html"
            });






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
          
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");


            });
            
        }
    }
}

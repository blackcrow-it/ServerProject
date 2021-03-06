﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ServerProject.Models;

namespace ServerProject
{
    using Microsoft.AspNetCore.Authentication.Cookies;

    using ReflectionIT.Mvc.Paging;

    using ServerProject.Middleware;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
                {

                    options.Cookie.HttpOnly = true;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc();
            services.AddPaging();
            services.AddDbContext<ServerProjectContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ServerProjectContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/accounts"), HandleMapCheckToken);
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/Grades"), HandleMapCheckToken);
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/Marks"), HandleMapCheckToken);
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/Courses"), HandleMapCheckToken);
            //app.UseWhen(context => context.Request.Path.StartsWithSegments("/Students"), HandleMapCheckToken);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        //private static void HandleMapCheckToken(IApplicationBuilder app)
        //{
        //    app.UseAuthenticationMiddleware();
        //}
    }
}

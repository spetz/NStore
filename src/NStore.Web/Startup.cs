using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NStore.Web.Framework;
using NStore.Web.ViewModels;

namespace NStore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppOptions>(Configuration.GetSection("app"));
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddSingleton<ProductsProvider>();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IOptions<AppOptions> appOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
            }

//            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseStaticFiles();
            app.UseCookiePolicy();

//            app.Use(async (ctx, next) =>
//            {
//                Console.WriteLine("BEFORE");
////                Console.WriteLine($"PATH: {ctx.Request.Path}");
//                await next();
//                Console.WriteLine("AFTER");
//            });
//            
//            app.Run(async ctx =>
//            {
//                Console.WriteLine("RUN");
//                await Task.CompletedTask;
//            });

            app.UseMvc();
        }
    }
}

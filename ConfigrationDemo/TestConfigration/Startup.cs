using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestConfigration
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Test configration

            //直接访问，节点由冒号(:)分隔
            var option1 = Configuration["option1"];
            var suboption1 = Configuration["subsection:suboption1"];
            var wizards_0 = Configuration["wizards:0:Name"];

            //多环境配置
            var envConfig = Configuration["EnvConfig"];

            //使用GetSection解析配置文件的节
            var subsectionOptions = Configuration.GetSection("subsection").Get<TestSubSectionConfig>();
            var suboption2 = subsectionOptions.SubOption2;

            Console.WriteLine($"option1: {option1}");
            Console.WriteLine($"subsection:suboption1: {suboption1}");
            Console.WriteLine($"wizards:0:Name: {wizards_0}");
            Console.WriteLine($"EnvConfig: {envConfig}");
            Console.WriteLine($"subsection:suboption2: {suboption2}");

            //注册到服务容器
            services.Configure<TestSubSectionConfig>(Configuration.GetSection("subsection"));
            //services.Configure<TestSubSectionConfig>(options =>
            //{
            //    options.SubOption1 = subsectionOptions["suboption1"];
            //    options.SubOption2 = subsectionOptions["suboption2"];
            // });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

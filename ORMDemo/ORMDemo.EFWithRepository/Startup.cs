using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ORMDemo.EFWithRepository.Infrastructure;

namespace ORMDemo.EFWithRepository
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
            var connectionString = @"Server=CD02SZV3600503\SQLEXPRESS;Database=BloggingWithRepository;Trusted_Connection=True;";
            services.AddDbContext<BloggingContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<BloggingUnitOfWork>();
            services.AddTransient(typeof(IBlogggingRepositoryBase<,>), typeof(BlogggingRepositoryBase<,>));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Swagger
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "API Docs",
                    Version = "v1",
                });
            });

            //Profiling
            services.AddMiniProfiler(options =>
                options.RouteBasePath = "/profiler"
            ).AddEntityFramework();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // profiling, url to see last profile check: http://localhost:56775/profiler/results
                app.UseMiniProfiler();
            }

            app.UseSwagger();

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                // index.html customizable downloadable here: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/src/Swashbuckle.AspNetCore.SwaggerUI/index.html
                // this custom html has miniprofiler integration
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("ORMDemo.EFWithRepository.SwaggerIndex.html");
            });

            app.UseMvc();
        }
    }
}

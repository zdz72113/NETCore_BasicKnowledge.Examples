using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SignalRJWTDemo.Authentication;
using SignalRJWTDemo.Hubs;
using SignalRJWTDemo.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRJWTDemo
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddTransient<IUserService, UserService>();

            #region Swagger
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "API Docs",
                    Version = "v1",
                });

                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                options.AddSecurityRequirement(security);
                options.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme
                {
                    Description = "Format: Bearer {auth_token}",
                    Name = "Authorization",
                    In = "header"
                });
            });
            #endregion

            #region Jwt token Authentication
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                    ValidateAudience = false,
                    ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = _signingKey,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.
                configureOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken) && (context.HttpContext.Request.Path.StartsWithSegments("/chatHub")))
                        //(context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseAuthentication();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvc();
        }
    }
}

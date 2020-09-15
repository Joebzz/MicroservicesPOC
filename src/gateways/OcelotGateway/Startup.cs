using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace OcelotGateway
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
            IdentityModelEventSource.ShowPII = true;

            var authenticationProviderKey = "TestKey";
            Action<IdentityServerAuthenticationOptions> options = o =>
            {
                o.RequireHttpsMetadata = false;
                o.Authority = "https://identityserver";
                o.ApiName = "GatewayAPI";
                o.SupportedTokens = SupportedTokens.Both;
            };

            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, options);

            //services.AddControllers();
            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.UseAuthentication();
            await app.UseOcelot();
        }
    }
}

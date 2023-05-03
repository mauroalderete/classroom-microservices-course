using Catalog.Persistence.Database;
using Catalog.Service.Queries;
using Common.Logging;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Catalog.API
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
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog"))
            );

            services.AddTransient<IProductQueryService, ProductQueryService>();

            services.AddMediatR(Assembly.Load("Catalog.Service.EventHandlers"));

            services.AddControllers();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddHealthChecksUI();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddProvider(new SysLoggerProvider(
                Configuration.GetValue<string>("Papertrail:host"),
                Configuration.GetValue<int>("Papertrail:port"),
                (msg, level) => level >= LogLevel.Information));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecksUI();
                endpoints.MapControllers();
            });
        }
    }
}

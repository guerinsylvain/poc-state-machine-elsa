using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Elsa.Activities.Email.Extensions;
using Elsa.Activities.Http.Extensions;
using Elsa.Activities.Timers.Extensions;
using Elsa.Dashboard.Extensions;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;
using Elsa.Persistence.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dashboard.Application.Activities;
using Dashboard.Application.Handlers;
using Elsa.Activities.Console.Extensions;
using Elsa.Extensions;

namespace Dashboard
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services
                // Add services used for the workflows runtime.
                .AddElsa(elsa => elsa.AddEntityFrameworkStores<SqlServerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("db"))))
                .AddConsoleActivities()
                .AddHttpActivities(options => options.Bind(Configuration.GetSection("Elsa:Http")))
                .AddEmailActivities(options => options.Bind(Configuration.GetSection("Elsa:Smtp")))
                //.AddTimerActivities(options => options.Bind(Configuration.GetSection("Elsa:Timers"))) // Timer activities are not used in this POC
                .AddActivities()
                .AddSingleton(Console.Out)

                // Add services used for the workflows dashboard.
                .AddElsaDashboard()

                // Add our liquid handler.
                .AddNotificationHandlers(typeof(LiquidConfigurationHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles()
               .UseHttpActivities()
               .UseRouting()
               .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute())
               .UseWelcomePage();
        }


    }
}

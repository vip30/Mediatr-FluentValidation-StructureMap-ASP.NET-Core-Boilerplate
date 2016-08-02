using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CQRSBoilerPlate.Services;
using MediatR;
using AutoMapper;
using System.Reflection;
using CQRSBoilerPlate.Domain;
using CQRSBoilerPlate.Entities.DBModels;
using CQRSBoilerPlate.Entities.Context;
using NLog.Extensions.Logging;
using StructureMap;
using System;
using FluentValidation;
using CQRSBoilerPlate.Infrastructure;
using CQRSBoilerPlate.ActionFilter;

namespace CQRSBoilerPlate
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly("CQRSBoilerPlate")
                ));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<CustomOpenIddictManager>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict<ApplicationUser, ApplicationDbContext>()
                .AllowPasswordFlow()
                .AllowRefreshTokenFlow()
                .AddEphemeralSigningKey()
                .DisableHttpsRequirement()
                .EnableTokenEndpoint("/connect/token")
                .UseJsonWebTokens();

            services.AddMvc(option =>
            {
                //For output validation error to json format 
                option.Filters.Add(typeof(ValidationExceptionHandlerErrorAttribute));
            });
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddAutoMapper(typeof(Domain.Startup));
            return ConfigureIoC(services);
        }

        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.Assembly(typeof(Domain.Startup).GetTypeInfo().Assembly);
                    _.WithDefaultConventions();
                    _.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
                    _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    _.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
                });
                var handlerType = config.For(typeof(IRequestHandler<,>));
                handlerType.DecorateAllWith(typeof(MediatorPipeline<,>));
                handlerType.DecorateAllWith(typeof(ValidatorHandler<,>));
                var asyncHandlerType = config.For(typeof(IAsyncRequestHandler<,>));
                asyncHandlerType.DecorateAllWith(typeof(AsyncMediatorPipeline<,>));
                asyncHandlerType.DecorateAllWith(typeof(AsyncValidatorHandler<,>));
                config.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
                config.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
                config.For<IMediator>().Use<Mediator>();

            });
            container.Populate(services);
            return container.GetInstance<IServiceProvider>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseCors(builder =>
               builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
           );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseOAuthValidation();

            app.UseStaticFiles();

            app.UseOpenIddict();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Audience = "http://localhost:50801/",
                Authority = "http://localhost:50801/"
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            env.ConfigureNLog("nlog.config");
        }
    }
}

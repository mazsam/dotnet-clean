using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Serilog;
using VendorBoilerplate.Application.Infrastructures;
using VendorBoilerplate.Application.Infrastructures.AutoMapper;
using VendorBoilerplate.Application.Interfaces;
using VendorBoilerplate.Application.Interfaces.Authorization;
using VendorBoilerplate.Application.Misc;
using VendorBoilerplate.Infrastructure.Authorization;
using VendorBoilerplate.Infrastructure.ErrorHandler;
using VendorBoilerplate.Infrastructure.Persistences;
using System.IO;
using System.Linq;
using System.Reflection;

namespace VendorBoilerplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            services.AddLogging(a => a.AddSerilog(logger));

            services.AddControllers();
            services.AddMemoryCache();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            services.AddSingleton<Utils>();
            services.AddHttpContextAccessor();

            services.Configure<FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 159715200;
                x.MultipartBoundaryLengthLimit = 159715200;
                x.MultipartHeadersLengthLimit = 159715200;
            });

            services.Configure<IISServerOptions>(a =>
            {
                a.MaxRequestBodySize = 159715200;
            });

            services.Configure<KestrelServerOptions>(a =>
            {
                a.Limits.MaxRequestBodySize = 159715200;
            });

            services.AddHttpClient();
            // Add DbContext using SQL Server Provider
            services.AddDbContext<ISFDDBContext, SFDDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("VendorDatabase")));

            // mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile(
                    services.BuildServiceProvider().GetService<VendorDBContext>(),
                    services.BuildServiceProvider().GetService<Utils>()
                ));
            });
            services.AddSingleton(mappingConfig.CreateMapper());
            services
               .AddMvc()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ForgotPasswordCommandValidator>())
               .AddNewtonsoftJson(x =>
               {
                   x.SerializerSettings.ContractResolver = new DefaultContractResolver
                   {
                       NamingStrategy = new SnakeCaseNamingStrategy()

                   };
                   x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
               })
               .ConfigureApiBehaviorOptions(options =>
               {
                   options.InvalidModelStateResponseFactory = c =>
                   {
                       var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                           .SelectMany(v => v.Errors)
                           .Select(v => v.ErrorMessage));
                       return new BadRequestObjectResult(new
                       {
                           ErrorCode = 400,
                           Message = errors
                       });
                   };
               });
            services.AddSingleton<IMemoryData, MemoryData>();
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            app.UseAuthme();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
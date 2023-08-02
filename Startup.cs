using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using VendorBoilerplate.Application.Infrastructures.AutoMapper;
using VendorBoilerplate.Application.Interfaces;
using VendorBoilerplate.Application.Misc;
using VendorBoilerplate.Infrastructure.ErrorHandler;
using VendorBoilerplate.Infrastructure.Persistences;
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
            services.AddControllers();
            services.AddMemoryCache();

            // Add MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMvc();
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
            services.AddDbContext<IVendorDBContext, VendorDBContext>(options =>
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

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
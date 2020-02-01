using AppOscar.API.Extensions;
using AppOscar.API.Repositories;
using AppOscar.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace AppOscar.API
{
    public class Startup
    {
        public IWebHostEnvironment _environment { get; }
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                var defaultPolicy = new CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build();

                option.AddDefaultPolicy(defaultPolicy);
            });

            services.AddDbContext<AppOscarContext>(opt => opt.UseInMemoryDatabase("AppOscarDB"));

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddMediatR(typeof(Startup));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddMvc(
                    opt => opt.EnableEndpointRouting = false
                )
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                // TODO: Variar esta versão conforme versão do assembly da API.
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppOscar API", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();

            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var appContext = serviceScope.ServiceProvider.GetService<AppOscarContext>();
                    appContext.SeedData();
                }
            }

            app.UseCors((x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Oscar API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}

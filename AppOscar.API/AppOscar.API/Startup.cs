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
using Microsoft.OpenApi.Models;
using System;

namespace AppOscar.API
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

            services.AddScoped<IFilmeRepository, FilmeRepository>();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var appContext = serviceScope.ServiceProvider.GetService<AppOscarContext>();
                    await appContext.SeedData();
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

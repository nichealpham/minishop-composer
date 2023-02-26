using AppGlobal.Filters;
using AppGlobal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace AppGlobal.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection ConfigureAPIServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            string appName = Configuration["ApplicationName"];
            Services.Configure<IConfiguration>(Configuration);
            Services.AddHttpClient();

            Services.AddSingleton<AccessTokenService>();
            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services
                .AddMvc(option => {
                    option.EnableEndpointRouting = false;
                    option.Filters.Add(new ApiExceptionFilter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = string.Format("{0}", appName),
                    Description = string.Format("{0} APIs", appName),
                    TermsOfService = new Uri("https://example.com/terms"),
                    License = new OpenApiLicense
                    {
                        Name = "GNU license",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

            return Services;
        }

        public static IApplicationBuilder ConfigureAPIApp(this IApplicationBuilder app, IConfiguration Configuration)
        {
            string appName = Configuration["ApplicationName"];

            app.UseHttpsRedirection();

            //app.UseCors("AllowAll");
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", string.Format("{0} V1", appName));
                c.RoutePrefix = string.Empty;
            });

            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(string.Format("Welcome to API MicroService!"));
                });
            });

            return app;
        }
    }
}

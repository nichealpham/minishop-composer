using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Business;
using AppGlobal.DBContext;
using AppGlobal.Extensions;
using AppGlobal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
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
            // 1. Configure API Microservice
            services.ConfigureAPIServices(Configuration);
            services.AddSingleton<FirebaseTokenValidator>();
            services.AddSingleton<FirebaseCloudMessaging>();
            services.AddSingleton<GoogleApiService>();

            // 2. Add DBContext
            services.AddDbContext<ERPContext>(opt => opt.UseSqlServer(Configuration["ConnectionString"]));

            // 3. Add Business
            services.AddScoped<ActivityBusiness>();
            services.AddScoped<UserBusiness>();
            services.AddScoped<AuthenBusiness>();
            services.AddScoped<ClinicBusiness>();
            services.AddScoped<ClinicServiceBusiness>();
            services.AddScoped<ClinicUserBusiness>();
            services.AddScoped<SearchBusiness>();
            services.AddScoped<AppointmentBusiness>();
            services.AddScoped<EpisodeBusiness>();
            services.AddScoped<ReportBusiness>();
            services.AddScoped<AssetBusiness>();
            services.AddScoped<PublicBusiness>();
            services.AddScoped<CategoryBusiness>();
            services.AddScoped<AttachmentBusiness>();

            services.AddControllers(options =>
            {

            });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureAPIApp(Configuration);

            app.UseEndpoints(endpoints =>
            {

            });
        }
    }
}

using System;
using System.IO;
using System.Net.Mime;
using Enigmachan.Models;
using enigmachan_backend.Services;
using enigmachan_backend.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Tickets
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
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<ThreadContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DatabaseContext")));
            services.AddScoped<IBoardService, BoardService>();
        }
        
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.UseMiddleware<RequestLogMiddleware>();
            if(env.IsDevelopment())
            {
            }

            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });
            app.UseRouting();
            app.UseCors(options =>
            {
                options.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .Build();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizServices.Data.EFCore;
using QuizServices.Models;

namespace QuizServices
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
            var connection = Configuration.GetConnectionString("QuizDatabase");

            services.AddEntityFrameworkSqlServer();

            services.AddDbContextPool<QuizContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(connection);
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            //Repository Pattern classes add
            services.AddScoped<EfCoreUsersRepository>();
            services.AddScoped<EfCoreClassesRepository>();
            services.AddScoped<EfCoreSubjectsRepository>();
            services.AddScoped<EfCoreClassesSubjectsRepository>();
            services.AddScoped<EfCoreQuestionsRepository>();

            //services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowOrigin",
            //        builder => builder.WithOrigins("http://localhost:3000"));
            //});
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("MyPolicy");
            //app.UseCors("AllowMyOrigin");
            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseMvc();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Safety.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Safety
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /* START CODE FOR CONNECTION STRING */
        /// <summary>
        /// 
        /// </summary>
        public IConfigurationRoot Configuration{ get; set; }
        //public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString{ get; private set; }

        //public Startup(IConfiguration configuration)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment configuration)
        {
            //Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(configuration.ContentRootPath)
                .AddJsonFile("appSettings.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            //Configuring Swagger
            services.AddSwaggerGen(w =>
            {
                w.SwaggerDoc("v1", new Info {Title="Security API", Description="Security Core Api" });

                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"Safety.xml";

                w.IncludeXmlComments(xmlPath);
            });

            //services.AddDbContext<SecurityContext>(options => 
            //options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //ConnectionString = Configuration.GetConnectionString("SecurityConnection");
            //ConnectionString = Configuration["ConnectionStrings:SecurityConnection"];
            ConnectionString = Configuration["Logging:ConnectionStrings:SecurityConnection"];
            
            //Configuring Swagger
            app.UseSwagger();
            app.UseSwaggerUI( w =>
            {
                w.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });
        }


        /* START CODE FOR CONNECTION STRING */
        /* END CODE FOR CONNECTION STRING */
    }
}

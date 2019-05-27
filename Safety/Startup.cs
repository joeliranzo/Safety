using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Safety
{
    public class Startup
    {
        /* START CODE FOR CONNECTION STRING */
        public IConfigurationRoot Configuration{ get; set; }
        //public IConfiguration Configuration { get; }

        public static string ConnectionString{ get; private set; }

        //public Startup(IConfiguration configuration)
        public Startup(IHostingEnvironment configuration)
        {
            //Configuration = configuration;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(configuration.ContentRootPath)
                .AddJsonFile("appSettings.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddDbContext<SecurityContext>(options => 
            //options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }


        /* START CODE FOR CONNECTION STRING */
        /* END CODE FOR CONNECTION STRING */
    }
}

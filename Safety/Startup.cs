using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Safety.Models;
using Safety.Options;
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
        public IConfigurationRoot Configuration { get; set; }
        //public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionString { get; private set; }

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
            var jwtSettings = new JwtSettings();
            Configuration.Bind(key: nameof(jwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddMvc();

            services.AddAuthentication(configureOptions: x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
                //options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "egehid.com.do",
                    ValidAudience = "egehid.com.do",
                    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero

                    //ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(key:Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    //ValidateIssuer = false,
                    //ValidateAudience = false,
                    //RequireExpirationTime = false,
                    //ValidateLifetime = true
                });

            //Configuring Swagger
            services.AddSwaggerGen(w =>
            {
                w.SwaggerDoc("v1", new Info { Title = "Security API", Description = "Security Core Api" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[0]}
                };

                w.AddSecurityDefinition(name: "Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using bearer scheme",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                w.AddSecurityRequirement(security);

                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"Safety.xml";

                w.IncludeXmlComments(xmlPath);
            });


            //Configurando authenticacion
            //var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

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
            app.UseSwaggerUI(w =>
           {
               w.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
           });

            //Configurando Authenticacion en runtime.
            app.UseAuthentication();
        }


        /* START CODE FOR CONNECTION STRING */
        /* END CODE FOR CONNECTION STRING */
    }
}

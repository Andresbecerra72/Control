using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Control.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



namespace Control.Web
{
          
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
                               
            //*******************
            public void ConfigureServices(IServiceCollection services)
            {

            // Add framework services.           
            services.AddDbContext<DataContext>(cfg =>
            {
                //**esta es la inyeccion de la conexion a base de datos del archivo appsetting.json
                cfg.UseMySql(this.Configuration.GetConnectionString("DefaultConnection"));


            });

            //inyeccion del alimentador
            services.AddTransient<SeedDB>();//AddTrasient se usa y se destruye
            //inyeccion del repositorio
            services.AddScoped<IRepository, Repository>();//AddScoped se usa y mantiene hasta cerrar el proyecto


            //****************

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}


///***************************
//    public class Startup
//    {
//        public Startup(IHostingEnvironment env)
//        {
//            var builder = new ConfigurationBuilder()
//                .SetBasePath(env.ContentRootPath)
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
//                .AddEnvironmentVariables();
//            Configuration = builder.Build();
//        }

//        public IConfigurationRoot Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.  
//        public void ConfigureServices(IServiceCollection services)
//        {
//            // Add framework services.  
//            services.AddMvc();
//            services.Add(new ServiceDescriptor(typeof(DataContext), new DataContext(Configuration.GetConnectionString("DefaultConnection"))));
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.  
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
//        {
//            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
//            loggerFactory.AddDebug();

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//                app.UseBrowserLink();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }

//            app.UseStaticFiles();

//            app.UseMvc(routes =>
//            {
//                routes.MapRoute(
//                    name: "default",
//                    template: "{controller=Home}/{action=Index}/{id?}");
//            });
//        }
//    }
//}  
namespace Control.Web
{
    using Control.Web.Data.Repositories;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")); //para conectar con AZURE / SATENA
                // cfg.UseMySql(this.Configuration.GetConnectionString("DefaultConnection"));     //para conectar con Mysql


            });

            //inyeccion del alimentador
            services.AddTransient<SeedDB>();//AddTrasient se usa y se destruye

            //codigo para configurar el password de los usuarios
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
                cfg.SignIn.RequireConfirmedEmail = true; //es para la confirmacion de usuarios que se registran
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 0;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredLength = 6;
            })
                .AddDefaultTokenProviders()//token de seguridad para confirmar por medio del correo
        .AddEntityFrameworkStores<DataContext>();



            //inyeccion del repositorio
            services.AddScoped<IPassangerRepository, PassangerRepository>();//AddScoped se usa y mantiene hasta cerrar el proyecto

            //inyeccion del repositorio
            services.AddScoped<IKiuReportRepository, KiuReportRepository>();//AddScoped se usa y mantiene hasta cerrar el proyecto

            //inyeccion del repositorio
            services.AddScoped<ICountryRepository, CountryRepository>();//AddScoped se usa y mantiene hasta cerrar el proyecto

            //inyeccion del UserHelper
            services.AddScoped<IUserHelper, UserHelper>();//AddScoped se usa y mantiene hasta cerrar el proyecto

            //inyeccion del MailHelper
            services.AddScoped<IMailHelper, MailHelper>();//AddScoped se usa y mantiene hasta cerrar el proyecto

            //***********************************OJO TOKEN****************************
            //uso de TOKEN de seguridad para el acceso al API
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = this.Configuration["Tokens:Issuer"],
                        ValidAudience = this.Configuration["Tokens:Audience"],
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this.Configuration["Tokens:Key"]))
                    };
                });



            //****************

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Redireccionamiento cuando el usuario no esta autorizado
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/NotAuthorized";
                options.AccessDeniedPath = "/Account/NotAuthorized";
            });



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //mientra se realizan pruebas..para productivo se debe quitar los comentarios
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");

            //}

            app.UseDeveloperExceptionPage();// MUESTRA ERRORES EN PRODUCTIVO
            app.UseDatabaseErrorPage();// MUESTRA ERRORES EN PRODUCTIVO
            app.UseStatusCodePagesWithReExecute("/error/{0}");//Codigo de pagina no existe Error 404
            app.UseStaticFiles();
            app.UseAuthentication(); //permite las autorizaciones
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


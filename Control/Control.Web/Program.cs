namespace Control.Web
{
    using Control.Web.Data;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }

        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDB>();
                seeder.SeedAsync().Wait();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

}
//*********************** CODIGO ORIGINAL*******************
//public class Program   //TODO: **pendiente codigo para que llene la base de datos con el alimentador SeedDB
//{
//    public static void Main(string[] args) //por aqui arranca la aplicacion WEB
//    {
//        CreateWebHostBuilder(args).Build().Run();
//    }

//    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//        WebHost.CreateDefaultBuilder(args)
//            .UseStartup<Startup>();
//}
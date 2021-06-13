using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolve.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PhotoBoom.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)// Autofac entegrasyonu ba�lang�c�
         .UseServiceProviderFactory(new AutofacServiceProviderFactory())
         .ConfigureContainer<ContainerBuilder>(builder =>
         {
             builder.RegisterModule(new AutofacBusinessModule());
         })//Autofac entegrasyonu biti�i
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });
    }
}

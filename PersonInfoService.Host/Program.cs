using System;
using PersonInfoService.Host.Code.Core;
using PersonInfoService.Host.Code.DummyDatabaseInitializer;

namespace PersonInfoService.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Initialize Automapper");
            AutoMapperBuilder.RegisterAutoMapper();
            Console.WriteLine("Seeding database");

            var t = new PersonInfoDatabaseInitializer();
            t.Seed();

            Console.WriteLine("Running service");
            var wcfProvider = new WcfServiceProvider();
            wcfProvider.RunService();
        }
    }
}
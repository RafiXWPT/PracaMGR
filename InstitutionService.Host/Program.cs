using System;
using InstitutionService.Host.Code.Core;
using InstitutionService.Host.Code.DummyDatabaseInitializer;

namespace InstitutionService.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing object builder");
            ObjectBuilder.Initialize();
            Console.WriteLine("Initialize Automapper");
            AutoMapperBuilder.RegisterAutoMapper();
            Console.WriteLine("Seeding database");

            var t = new InstitutionDatabaseInitializer();
            t.Seed();

            Console.WriteLine("Running service");
            var wcfProvider = new WcfServiceProvider();
            wcfProvider.RunService();
        }
    }
}
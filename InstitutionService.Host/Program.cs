using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using InstitutionService.Host.Code.Core;
using InstitutionService.Host.Code.DataAccessLayer;
using InstitutionService.Host.Code.DummyDatabaseInitializer;

namespace InstitutionService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing object builder");
            ObjectBuilder.Initialize();
            Console.WriteLine("Initialize Automapper");
            AutoMapperBuilder.RegisterAutoMapper();
            Console.WriteLine("Seeding database");

            var t = new InstitutionDatabaseInitializer(ObjectBuilder.Container.GetInstance<IDatabaseContext>());
            t.Seed();
            
            Console.WriteLine("Running service");
            var wcfProvider = new WcfServiceProvider();
            wcfProvider.RunService();
        }
    }
}

﻿using System;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Domain;
using InstitutionService;
using PersonInfoService.Host.Code.DataAccessLayer;

namespace PersonInfoService.Host.Code.Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    internal class PersonInfoService : IPersonInfoService
    {
        public bool Ping()
        {
            return true;
        }

        public PersonTransferObject GetPersonInfo(string pesel)
        {
            Console.WriteLine("Pobranie informacji o konkretnej osobie");
            var db = new PersonInfoServiceDatabaseContext();
            var person = db.Persons.FirstOrDefault(p => p.Pesel == pesel);
            return Mapper.Map<PersonTransferObject>(person);
        }
    }
}
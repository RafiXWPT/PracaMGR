using System;
using System.Collections.Generic;
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

        public List<PersonTransferObject> FilterPersons(string pesel = null, string firstName = null, string lastName = null, string insuranceId = null, DateTime? birthDate = null)
        {
            Console.WriteLine("Filtrowanie osób");
            var db = new PersonInfoServiceDatabaseContext();

            var persons = db.Persons.Where(p =>
                (pesel == null || p.Pesel.Contains(pesel)) &&
                (firstName == null || p.FirstName.Contains(firstName)) &&
                (lastName == null || p.LastName.Contains(lastName)) &&
                (insuranceId == null || p.InsuranceId.Contains(insuranceId)) &&
                (birthDate == null || (p.BirthDate.Day == birthDate.Value.Day &&
                                       p.BirthDate.Month == birthDate.Value.Month &&
                                       p.BirthDate.Year == birthDate.Value.Year))).ToList();

            return persons.Select(Mapper.Map<PersonTransferObject>).ToList();
        }
    }
}
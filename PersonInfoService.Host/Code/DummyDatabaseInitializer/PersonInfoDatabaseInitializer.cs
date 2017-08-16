using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using PersonInfoService.Host.Code.DataAccessLayer;

namespace PersonInfoService.Host.Code.DummyDatabaseInitializer
{
    public class PersonInfoDatabaseInitializer
    {
        public void Seed()
        {
            var personRepository = new PersonInfoServiceDatabaseContext();

            if (personRepository.Persons.Any())
                return;

            AddPersons(personRepository);
        }

        private void AddPersons(PersonInfoServiceDatabaseContext repository)
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "93070114133",
                    FirstName = "Rafał",
                    SecondName = "Palej",
                    BirthDate = new DateTime(1993, 7, 1),
                    InsuranceId = "259024",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Ogrodowa",
                        HomeNumber = "109"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "24020809720",
                    FirstName = "Anna",
                    SecondName = "Nowak",
                    BirthDate = new DateTime(1924, 2, 8),
                    InsuranceId = "259001",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "30-006",
                        City = "Kraków",
                        Street = "Wrocławska",
                        HomeNumber = "13"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "62012210646",
                    FirstName = "Bogusław",
                    SecondName = "Zbych",
                    BirthDate = new DateTime(1962, 1, 22),
                    InsuranceId = "259234",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-300",
                        City = "Nowy Sącz",
                        Street = "Tajemnicza",
                        HomeNumber = "42"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "64121311834",
                    FirstName = "Adam",
                    SecondName = "Nawałka",
                    BirthDate = new DateTime(1964, 12, 13),
                    InsuranceId = "215469",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "27-120",
                        City = "Tarnów",
                        Street = "Wiejska",
                        HomeNumber = "33"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "26112804811",
                    FirstName = "Teodor",
                    SecondName = "Krawczyk",
                    BirthDate = new DateTime(1926, 11, 28),
                    InsuranceId = "556125",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "17-270",
                        City = "Lipnica",
                        Street = "Czysta",
                        HomeNumber = "42"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "61061910064",
                    FirstName = "Maria",
                    SecondName = "Dadał",
                    BirthDate = new DateTime(1961, 6, 19),
                    InsuranceId = "221545",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Rolanda",
                        HomeNumber = "41"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "26031604565",
                    FirstName = "Ewa",
                    SecondName = "Palej",
                    BirthDate = new DateTime(1926, 3, 16),
                    InsuranceId = "996565",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Ogrodowa",
                        HomeNumber = "109"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "03300900942",
                    FirstName = "Filip",
                    SecondName = "Gościński",
                    BirthDate = new DateTime(2003, 3, 1),
                    InsuranceId = "259024",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "40-170",
                        City = "Krynica-Zdrój",
                        Street = "Niewiadoma",
                        HomeNumber = "220"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "89021303161",
                    FirstName = "Kinga",
                    SecondName = "Szepielak",
                    BirthDate = new DateTime(1989, 2, 13),
                    InsuranceId = "5124585",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "22-180",
                        City = "Tarnów",
                        Street = "Wola Rzędzińska",
                        HomeNumber = "530"
                    }
                },
                new Person
                {
                    Id = Guid.NewGuid(),
                    Pesel = "60092601196",
                    FirstName = "Sarys",
                    SecondName = "Sire",
                    BirthDate = new DateTime(1960, 9, 26),
                    InsuranceId = "221546",
                    Address = new Address
                    {
                        Id = Guid.NewGuid(),
                        Country = "Tibia",
                        Province = "Mainland",
                        ZipCode = "00-001",
                        City = "Venore",
                        Street = "Depo",
                        HomeNumber = "1"
                    }
                }
            };

            persons.ForEach(s => repository.Persons.Add(s));
            repository.SaveChanges();
        }
    }
}
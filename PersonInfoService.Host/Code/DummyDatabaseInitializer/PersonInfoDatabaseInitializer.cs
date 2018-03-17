using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using PersonInfoService.Host.Code.DataAccessLayer;

namespace PersonInfoService.Host.Code.DummyDatabaseInitializer
{
    public class PersonInfoDatabaseInitializer
    {
        private readonly List<string> Pesels = new List<string>
        {
            "83594287231",
            "70547639391",
            "15701222746",
            "99080857898",
            "40135518027",
            "29160216833",
            "63076140566",
            "18220269708",
            "34894580293",
            "11574723415",
            "85738754675",
            "58295164186",
            "57808417687",
            "34781667375",
            "65107018045",
            "21487220298",
            "70708545175",
            "49349469423",
            "84896349546",
            "52641897748",
            "86324249779",
            "46475978609",
            "51912346934",
            "94824247606",
            "86037750564",
            "28339871989",
            "33472599046",
            "13744691289",
            "91201816108",
            "49091010279",
            "50180591403",
            "48535788916",
            "61929380392",
            "22308971712",
            "28698118387",
            "67663528331",
            "70305848421",
            "78529648497",
            "69768254881",
            "94355199750",
            "87421959295",
            "24359070470",
            "76839319824",
            "13288281465",
            "15739123712",
            "59386647842",
            "95017539223",
            "11903526667",
            "57906985549",
            "38488780154",
            "72543282028",
            "44824310430",
            "76519888809",
            "61203695654",
            "28200377144",
            "42006294645",
            "67289171157",
            "62667165652",
            "90738570740",
            "77171710007",
            "57393638016",
            "39206245180",
            "19175881767",
            "17054665659",
            "75819416155",
            "62880197276",
            "18120765946",
            "12398858601",
            "29292042974",
            "74575971718",
            "86194672429",
            "25606240807",
            "69665088428",
            "89476716083",
            "98001794551",
            "36474747018",
            "63246178415",
            "80078698781",
            "13175337213",
            "84543919629",
            "28840394227",
            "14244823194",
            "80081258972",
            "43769035861",
            "61536584920",
            "73255734370",
            "79822727439",
            "24863485387",
            "37972961115",
            "40103167429",
            "39178753806",
            "94723181514",
            "62490871923",
            "69125275406",
            "95272813676",
            "71655143385",
            "54908410861",
            "65224117585",
            "78189181916",
            "37455697470"
        };

        private readonly List<string> FirstNames = new List<string>
        {
            "Rafał",
            "Darek",
            "Zbigniew",
            "Dominik",
            "Tomek",
            "Kamil",
            "Adrian",
            "Michał",
            "Jacek",
            "Joanna",
            "Ewa",
            "Roksana",
            "Diana",
            "Dorota",
            "Marzena",
            "Karolina",
            "Katarzyna"
        };

        private readonly List<string> LastNmaes = new List<string>
        {
            "Filar",
            "Auto",
            "Szpic",
            "Dobrowolski",
            "Palej",
            "Dominik",
            "Rondo",
            "Konar",
            "Zapałka",
            "Koniczyna",
            "Kwiat",
            "Mleko"
        };

        private readonly List<string> Cities = new List<string>
        {
            "Nowy Sącz",
            "Krynica-Zdrój",
            "Kraków",
            "Muszyna",
            "Tarnów",
            "Nowy Targ",
            "Zakopane",
            "Katowice"
        };

        private readonly List<string> Streets = new List<string>
        {
            "Ogrodowa",
            "Leśna",
            "Kolorowa",
            "Ciemna",
            "Kręta",
            "Długa",
            "Wrocławska",
            "Krowodrza",
            "Glogera"
        };

        private readonly Random _random = new Random();

        public void Seed()
        {
            var personRepository = new PersonInfoServiceDatabaseContext();

            if (personRepository.Persons.Any())
                return;

            AddPersons(personRepository);
        }

        private void AddPersons(PersonInfoServiceDatabaseContext repository)
        {
            var persons = new List<Person>();
            foreach(var pesel in Pesels)
            {
                persons.Add(new Person
                {
                    PersonId = Guid.NewGuid(),
                    Pesel = pesel,
                    FirstName = FirstNames[_random.Next(0, FirstNames.Count-1)],
                    LastName = LastNmaes[_random.Next(0, LastNmaes.Count-1)],
                    BirthDate = new DateTime(_random.Next(1900,2018), _random.Next(1,12), _random.Next(1,28)),
                    InsuranceId = _random.Next(100000, 999999).ToString(),
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Polska",
                        Province = "Małopolska",
                        ZipCode = $"{_random.Next(10,99)}-{_random.Next(100,999)}",
                        City = Cities[_random.Next(0, Cities.Count-1)],
                        Street = Streets[_random.Next(0,Streets.Count-1)],
                        HomeNumber = $"{_random.Next(1,1000)}"
                    }
                });
            }    

            persons.ForEach(s => repository.Persons.Add(s));
            repository.SaveChanges();
        }
    }
}
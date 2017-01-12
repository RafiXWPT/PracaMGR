using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Hospitalization;
using Domain.Interfaces;
using Domain.Inventory;
using InstitutionService.Host.Code.Core;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DummyDatabaseInitializer
{
    public class InstitutionDatabaseInitializer
    {
        private readonly InstitutionServiceDatabaseContext _context;
        private Random _rnd = new Random();
        private List<Medicine> _medicines = new List<Medicine>();
        private List<Patient> _patients = new List<Patient>();
        private List<Hospitalization> _hospitalizations = new List<Hospitalization>();
        private List<Examination> _examinations = new List<Examination>();
        private List<Treatment> _treatments = new List<Treatment>();
        private List<UsedMedicine> _usedMedicines = new List<UsedMedicine>();

        public InstitutionDatabaseInitializer(IDatabaseContext context)
        {
            _context = context as InstitutionServiceDatabaseContext;
        }

        public void Seed()
        {
            if (_context.Patients.Any())
                return;

            AddMedicines();
            AddPatients();
            AddHospitalizations();
            AddExaminations();
            AddTreatments();
            AddUsedMedicines();
        }

        private void AddMedicines()
        {
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Witamina C" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Rutinoskorbin" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Morfina" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Duomox" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Sudafed" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Aspiryna" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Gripex" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Cholinex" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Apap" });
            _medicines.Add(new Medicine() { MedicineId = Guid.NewGuid(), MedicineName = "Soda" });


            _medicines.ForEach(s => _context.Medicines.Add(s));
            _context.SaveChanges();
        }

        private void AddPatients()
        {
            var patients = new List<Patient>
            {
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "93070114133",
                    FirstName = "Rafał",
                    SecondName = "Palej",
                    BirthDate = new DateTime(1993, 7, 1),
                    InsuranceId = "259024",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Ogrodowa",
                        HomeNumber = "109"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "24020809720",
                    FirstName = "Anna",
                    SecondName = "Nowak",
                    BirthDate = new DateTime(1924,2,8),
                    InsuranceId = "259001",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "30-006",
                        City = "Kraków",
                        Street = "Wrocławska",
                        HomeNumber = "13"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "62012210646",
                    FirstName = "Bogusław",
                    SecondName = "Zbych",
                    BirthDate = new DateTime(1962,1,22),
                    InsuranceId = "259234",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-300",
                        City = "Nowy Sącz",
                        Street = "Tajemnicza",
                        HomeNumber = "42"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "64121311834",
                    FirstName = "Adam",
                    SecondName = "Nawałka",
                    BirthDate = new DateTime(1964, 12, 13),
                    InsuranceId = "215469",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "27-120",
                        City = "Tarnów",
                        Street = "Wiejska",
                        HomeNumber = "33"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "26112804811",
                    FirstName = "Teodor",
                    SecondName = "Krawczyk",
                    BirthDate = new DateTime(1926,11,28),
                    InsuranceId = "556125",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "17-270",
                        City = "Lipnica",
                        Street = "Czysta",
                        HomeNumber = "42"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "61061910064",
                    FirstName = "Maria",
                    SecondName = "Dadał",
                    BirthDate = new DateTime(1961,6,19),
                    InsuranceId = "221545",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Rolanda",
                        HomeNumber = "41"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "26031604565",
                    FirstName = "Ewa",
                    SecondName = "Palej",
                    BirthDate = new DateTime(1926,3,16),
                    InsuranceId = "996565",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "33-370",
                        City = "Muszyna",
                        Street = "Ogrodowa",
                        HomeNumber = "109"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "03300900942",
                    FirstName = "Filip",
                    SecondName = "Gościński",
                    BirthDate = new DateTime(2003,3,1),
                    InsuranceId = "259024",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "40-170",
                        City = "Krynica-Zdrój",
                        Street = "Niewiadoma",
                        HomeNumber = "220"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "89021303161",
                    FirstName = "Kinga",
                    SecondName = "Szepielak",
                    BirthDate = new DateTime(1989,2,13),
                    InsuranceId = "5124585",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Poland",
                        Province = "Małopolska",
                        ZipCode = "22-180",
                        City = "Tarnów",
                        Street = "Wola Rzędzińska",
                        HomeNumber = "530"
                    }
                },
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "60092601196",
                    FirstName = "Sarys",
                    SecondName = "Sire",
                    BirthDate = new DateTime(1960,9,26),
                    InsuranceId = "221546",
                    Address = new Address
                    {
                        AddressId = Guid.NewGuid(),
                        Country = "Tibia",
                        Province = "Mainland",
                        ZipCode = "00-001",
                        City = "Venore",
                        Street = "Depo",
                        HomeNumber = "1"
                    }
                }
            };

            var takeCount = _rnd.Next(3, patients.Count);
            for (var i = 0; i < takeCount; i++)
            {
                var tmpPerson = patients[_rnd.Next(0, patients.Count)];
                if (!_patients.Contains(tmpPerson))
                {
                    _patients.Add(tmpPerson);
                }
                else
                    i--;
            }

            _patients.ForEach(s => _context.Patients.Add(s));
            _context.SaveChanges();
        }

        private void AddHospitalizations()
        {
            foreach (var patient in _patients)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfHospitalizationBegin = GenRandomDate(patient.BirthDate);
                    var dateOfHospitalizationEnd = dateOfHospitalizationBegin.AddDays(_rnd.Next(1, 14));
                    _hospitalizations.Add(new Hospitalization() { HospitalizationId = Guid.NewGuid(), PatientId = patient.PatientId, HospitalizationStartTime = dateOfHospitalizationBegin, HospitalizationEndTime = dateOfHospitalizationEnd });
                }
            }

            _hospitalizations.ForEach(s => _context.Hospitalizations.Add(s));
            _context.SaveChanges();
        }

        private void AddExaminations()
        {
            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfExaminationBegin = GenRandomDate(hospitalization.HospitalizationStartTime);
                    var dateOfExaminationEnd = dateOfExaminationBegin.AddHours(_rnd.Next(1, 6));
                    _examinations.Add(new Examination() { ExaminationId = Guid.NewGuid(), HospitalizationId = hospitalization.HospitalizationId, ExaminationStartTime = dateOfExaminationBegin, ExaminationEndTime = dateOfExaminationEnd, ExaminationDetails = Guid.NewGuid().ToString() });
                }
            }

            _examinations.ForEach(s => _context.Examinations.Add(s));
            _context.SaveChanges();
        }

        private void AddTreatments()
        {
            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 5);
                for (var i = 0; i < takeCount; i++)
                {
                    _treatments.Add(new Treatment() { TreatmentId = Guid.NewGuid(), HospitalizationId = hospitalization.HospitalizationId, TreatmentDateTime = GenRandomDate(hospitalization.HospitalizationStartTime, hospitalization.HospitalizationEndTime) });
                }
            }

            _treatments.ForEach(s => _context.Treatments.Add(s));
            _context.SaveChanges();
        }

        private void AddUsedMedicines()
        {
            foreach (var treatment in _treatments)
            {
                var takeCount = _rnd.Next(0, 20);
                for (var i = 0; i < takeCount; i++)
                {
                    _usedMedicines.Add(new UsedMedicine() { UsedMedicineId = Guid.NewGuid(), TreatmentId = treatment.TreatmentId, MedicineId = _medicines[_rnd.Next(0, _medicines.Count-1)].MedicineId, Dose = GetRandomNumber(1.0, 1000.0) });
                }
            }

            _usedMedicines.ForEach(s => _context.UsedMedicines.Add(s));
            _context.SaveChanges();
        }

        private DateTime GenRandomDate(DateTime min)
        {
            return GenRandomDate(min, DateTime.Now);
        }

        private DateTime GenRandomDate(DateTime min, DateTime max)
        {
            var maxDays = (max - min).Days;
            return min.AddDays(_rnd.Next(1, maxDays));
        }
        private double GetRandomNumber(double min, double max)
        {
            return _rnd.NextDouble() * (max - min) + min;
        }
    }
}

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
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DummyDatabaseInitializer
{
    public class InstitutionDatabaseInitializer
    {
        private Random _rnd = new Random();
        private List<Medicine> _medicines = new List<Medicine>();
        private List<Patient> _patients = new List<Patient>();
        private List<Hospitalization> _hospitalizations = new List<Hospitalization>();
        private List<Examination> _examinations = new List<Examination>();
        private List<Treatment> _treatments = new List<Treatment>();
        private List<UsedMedicine> _usedMedicines = new List<UsedMedicine>();
        public void Seed(IDatabaseContext context)
        {
            var dbContext = context as InstitutionServiceDatabaseContext;
            AddPersons(dbContext);
        }

        private void AddPersons(InstitutionServiceDatabaseContext context)
        {
            if (context.Patients.Any())
                return;

            _patients = new List<Patient>
            {
                new Patient()
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = "93070114133",
                    FirstName = "Rafał",
                    SecondName = "Palej",
                    BirthDate = new DateTime(1993, 7, 1),
                    InsuranceId = "259024",
                }
            };

            _patients.ForEach(s => context.Patients.Add(s));
            context.SaveChanges();
        }

        /*private void AddHospitalizations(InstitutionServiceDatabaseContext context)
        {
            foreach (var person in _persons)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfHospitalizationBegin = GenRandomDate(person.BirthDate);
                    var dateOfHospitalizationEnd = dateOfHospitalizationBegin.AddDays(_rnd.Next(1, 14));
                    _hospitalizations.Add(new Hospitalization() { HospitalizationID = i, PersonID = person.PersonID, HospitalizationBegin = dateOfHospitalizationBegin, HospitalizationEnd = dateOfHospitalizationEnd });
                }
            }

            _hospitalizations.ForEach(s => context.Hospitalizations.Add(s));
            context.SaveChanges();
        }

        private void AddExaminations(InstitutionServiceDatabaseContext context)
        {
            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfExaminationBegin = GenRandomDate(hospitalization.HospitalizationBegin);
                    var dateOfExaminationEnd = dateOfExaminationBegin.AddHours(_rnd.Next(1, 6));
                    _examinations.Add(new Examination() { ExaminationID = i, HospitalizationID = hospitalization.HospitalizationID, ExaminationBegin = dateOfExaminationBegin, ExaminatitonEnd = dateOfExaminationEnd, ExaminationDetails = Guid.NewGuid().ToString() });
                }
            }

            _examinations.ForEach(s => context.Examinations.Add(s));
            context.SaveChanges();
        }

        private void AddTreatments(InstitutionServiceDatabaseContext context)
        {
            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 5);
                for (var i = 0; i < takeCount; i++)
                {
                    _treatments.Add(new Treatment() { TreatmentID = i, HospitalizationID = hospitalization.HospitalizationID, TreatmentDate = GenRandomDate(hospitalization.HospitalizationBegin, hospitalization.HospitalizationEnd) });
                }
            }

            _treatments.ForEach(s => context.Treatments.Add(s));
            context.SaveChanges();
        }

        private void AddUsedMedicines(InstitutionServiceDatabaseContext context)
        {
            foreach (var treatment in _treatments)
            {
                var takeCount = _rnd.Next(0, 20);
                for (var i = 0; i < takeCount; i++)
                {
                    _usedMedicines.Add(new UsedMedicine() { UsedMedicineId = i, TreatmentId = treatment.TreatmentId, MedicineId = _rnd.Next(1, _medicines.Count), Dose = GetRandomNumber(1.0, 1000.0) });
                }
            }

            _usedMedicines.ForEach(s => context.UsedMedicines.Add(s));
            context.SaveChanges();
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
        }*/
    }
}

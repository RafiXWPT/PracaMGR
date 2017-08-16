using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Inventory;
using Domain.Residence;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DummyDatabaseInitializer
{
    public class InstitutionDatabaseInitializer
    {
        private readonly List<Examination> _examinations = new List<Examination>();
        private readonly List<Hospitalization> _hospitalizations = new List<Hospitalization>();
        private readonly List<Medicine> _medicines = new List<Medicine>();
        private readonly List<Patient> _patients = new List<Patient>();
        private readonly Random _rnd = new Random();
        private readonly List<Treatment> _treatments = new List<Treatment>();
        private readonly List<UsedMedicine> _usedMedicines = new List<UsedMedicine>();

        public void Seed()
        {
            var patientRepository = new InstitutionServiceDatabaseContext("InstitutionContext");

            if (patientRepository.Patients.Any())
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
            var medicineRepository = new InstitutionServiceDatabaseContext("InstitutionContext");

            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Witamina C"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Rutinoskorbin"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Morfina"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Duomox"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Sudafed"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Aspiryna"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Gripex"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Cholinex"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Apap"});
            _medicines.Add(new Medicine {Id = Guid.NewGuid(), MedicineName = "Soda"});


            _medicines.ForEach(s => medicineRepository.Medicines.Add(s));
            medicineRepository.SaveChanges();
        }

        private void AddPatients()
        {
            var patientRepository =
                new InstitutionServiceDatabaseContext(
                    "InstitutionContext"); //= ObjectBuilder.Container.GetInstance<IPatientRepository>();

            var patients = new List<Patient>
            {
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "93070114133"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "24020809720"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "62012210646"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "64121311834"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "26112804811"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "61061910064"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "26031604565"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "03300900942"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "89021303161"
                },
                new Patient
                {
                    Id = Guid.NewGuid(),
                    Pesel = "60092601196"
                }
            };

            var takeCount = _rnd.Next(3, patients.Count);
            for (var i = 0; i < takeCount; i++)
            {
                var tmpPerson = patients[_rnd.Next(0, patients.Count)];
                if (!_patients.Contains(tmpPerson))
                    _patients.Add(tmpPerson);
                else
                    i--;
            }

            _patients.ForEach(s => patientRepository.Patients.Add(s));
            patientRepository.SaveChanges();
        }

        private void AddHospitalizations()
        {
            var hospitalizationRepository =
                new InstitutionServiceDatabaseContext(
                    "InstitutionContext"); //= ObjectBuilder.Container.GetInstance<IHospitalizationRepository>();

            foreach (var patient in _patients)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var st = new DateTime(1995, 1, 1);

                    var dateOfHospitalizationBegin = GenRandomDate(st.AddDays(_rnd.Next((DateTime.Today - st).Days)));
                    var dateOfHospitalizationEnd = dateOfHospitalizationBegin.AddDays(_rnd.Next(1, 14));
                    _hospitalizations.Add(new Hospitalization
                    {
                        Id = Guid.NewGuid(),
                        PatientId = patient.Id,
                        HospitalizationStartTime = dateOfHospitalizationBegin,
                        HospitalizationEndTime = dateOfHospitalizationEnd
                    });
                }
            }

            _hospitalizations.ForEach(s => hospitalizationRepository.Hospitalizations.Add(s));
            hospitalizationRepository.SaveChanges();
        }

        private void AddExaminations()
        {
            var examinationRepository =
                new InstitutionServiceDatabaseContext(
                    "InstitutionContext"); //= ObjectBuilder.Container.GetInstance<IExaminationRepository>();

            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfExaminationBegin = GenRandomDate(hospitalization.HospitalizationStartTime);
                    var dateOfExaminationEnd = dateOfExaminationBegin.AddHours(_rnd.Next(1, 6));
                    _examinations.Add(new Examination
                    {
                        Id = Guid.NewGuid(),
                        HospitalizationId = hospitalization.Id,
                        ExaminationStartTime = dateOfExaminationBegin,
                        ExaminationEndTime = dateOfExaminationEnd,
                        ExaminationDetails = Guid.NewGuid().ToString()
                    });
                }
            }

            _examinations.ForEach(s => examinationRepository.Examinations.Add(s));
            examinationRepository.SaveChanges();
        }

        private void AddTreatments()
        {
            var treatmentRepository =
                new InstitutionServiceDatabaseContext(
                    "InstitutionContext"); //= ObjectBuilder.Container.GetInstance<ITreatmentRepository>();

            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 5);
                for (var i = 0; i < takeCount; i++)
                    _treatments.Add(new Treatment
                    {
                        Id = Guid.NewGuid(),
                        HospitalizationId = hospitalization.Id,
                        TreatmentDateTime = GenRandomDate(hospitalization.HospitalizationStartTime,
                            hospitalization.HospitalizationEndTime)
                    });
            }

            _treatments.ForEach(s => treatmentRepository.Treatments.Add(s));
            treatmentRepository.SaveChanges();
        }

        private void AddUsedMedicines()
        {
            var usedMedicineRepository =
                new InstitutionServiceDatabaseContext(
                    "InstitutionContext"); //= ObjectBuilder.Container.GetInstance<IUsedMedicineRepository>();
            foreach (var treatment in _treatments)
            {
                var takeCount = _rnd.Next(0, 20);
                for (var i = 0; i < takeCount; i++)
                    _usedMedicines.Add(new UsedMedicine
                    {
                        Id = Guid.NewGuid(),
                        TreatmentId = treatment.Id,
                        MedicineId = _medicines[_rnd.Next(0, _medicines.Count - 1)].Id,
                        Dose = GetRandomNumber(1.0, 1000.0)
                    });
            }

            _usedMedicines.ForEach(s => usedMedicineRepository.UsedMedicines.Add(s));
            usedMedicineRepository.SaveChanges();
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using Domain.Residence;
using InstitutionService.Host.Code.Core;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.DummyDatabaseInitializer
{
    public class InstitutionDatabaseInitializer
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

        private readonly List<string> FirstPersonelNames = new List<string>
        {
            "Zbigniew",
            "Dominik",
            "Robert",
            "Oleksy",
            "Igor",
            "Damian",
            "Bartosz",
            "Krzysztof",
            "Rafał",
            "Dawid",
            "Elżbieta",
            "Dominika",
            "Aleksandra",
            "Joanna",
            "Kinga",
            "Beata",
            "Diana"
        };

        private readonly List<string> LastPersonelNames = new List<string>
        {   
            "Polak",
            "Rzemieślnik",
            "Data",
            "Szost",
            "Zasadni",
            "Cyrulik",
            "Tomczyk",
            "Lech",
            "Szepielak",
            "Gościński",
            "Popko",
            "Liść",
            "Brzoza",
            "Kwiat",
            "Opel"
        };

        private readonly List<string> UsedDevices = new List<string>
        {
            "Patyczki higieniczne",
            "Ciśnieniomierz",
            "Skalpel",
            "Nożyczki",
            "Szkło powiększające",
            "Wkładka termiczna",
            "Termometr",
            "Zaciskacz",
            "Gips",
            "Parownica",
            "Rurka z kamerą",
            "Lupa",
            "USG",
            "EKG",
            "Rentgen"
        };

        private readonly List<string> Documents = new List<string>
        {
            "sampleDoc1.docx",
            "sampleDoc2.docx",
            "sampleDoc3.docx",
            "sampleDoc4.docx",
            "sampleDoc5.docx",
            "sampleDoc6.pdf",
            "sampleDoc7.pdf",
            "sampleDoc8.pdf",
            "sampleDoc9.pdf",
            "sampleDoc10.pdf",
        };

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
            var medicineRepository = ObjectBuilder.Container.GetInstance<IRepository<Medicine>>();

            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Witamina C"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Rutinoskorbin"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Morfina"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Duomox"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Sudafed"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Aspiryna"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Gripex"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Cholinex"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Apap"});
            _medicines.Add(new Medicine {MedicineId = Guid.NewGuid(), MedicineName = "Soda"});


            _medicines.ForEach(s => medicineRepository.Create(s));
        }

        private void AddPatients()
        {
            var patientRepository = ObjectBuilder.Container.GetInstance<IRepository<Patient>>();

            var patients = new List<Patient>();
            foreach (var pesel in Pesels)
            {
                patients.Add(new Patient
                {
                    PatientId = Guid.NewGuid(),
                    Pesel = pesel
                });
            }
           
            var takeCount = _rnd.Next(25, patients.Count);
            for (var i = 0; i < takeCount; i++)
            {
                var tmpPerson = patients[_rnd.Next(0, patients.Count)];
                if (!_patients.Contains(tmpPerson))
                {
                    Console.WriteLine($"Adding Patients to Institution");
                    _patients.Add(tmpPerson);
                    patientRepository.Create(tmpPerson);
                    Console.WriteLine($"Entity {tmpPerson.PatientId} created");
                }
                else
                {
                    i--;
                }

            }
        }

        private void AddHospitalizations()
        {
            var hospitalizationRepository = ObjectBuilder.Container.GetInstance<IRepository<Hospitalization>>();

            foreach (var patient in _patients)
            {
                var takeCount = _rnd.Next(1, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var st = new DateTime(1995, 1, 1);

                    var dateOfHospitalizationBegin = GenRandomDate(st.AddDays(_rnd.Next((DateTime.Today - st).Days)));
                    var dateOfHospitalizationEnd = dateOfHospitalizationBegin.AddDays(_rnd.Next(1, 14));

                    var randomDocumentsCount = _rnd.Next(1, 10);
                    var documents = new List<HospitalizationDocument>();
                    for (var j = 0; j < randomDocumentsCount; j++)
                    {
                        var rndDocument = Documents[_rnd.Next(0, Documents.Count - 1)];
                        var rndDocumentBytes = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}/SampleDocuments/{rndDocument}");
                        documents.Add(new HospitalizationDocument
                        {
                            HospitalizationDocumentId = Guid.NewGuid(),
                            Name = rndDocument,
                            Content = rndDocumentBytes
                        });
                    }

                    var hospitalization = new Hospitalization
                    {
                        HospitalizationId = Guid.NewGuid(),
                        PatientId = patient.PatientId,
                        HospitalizationStartTime = dateOfHospitalizationBegin,
                        HospitalizationEndTime = dateOfHospitalizationEnd,
                        HospitalizationDocuments = documents
                    };

                    Console.WriteLine($"Adding Hospitalization to Patient {patient.PatientId}");
                    _hospitalizations.Add(hospitalization);
                    hospitalizationRepository.Create(hospitalization);
                    Console.WriteLine($"Entity {hospitalization.HospitalizationId} created");
                }
            }
        }

        private void AddExaminations()
        {
            var examinationRepository = ObjectBuilder.Container.GetInstance<IRepository<Examination>>();

            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(1, 5);
                for (var i = 0; i < takeCount; i++)
                {
                    var dateOfExaminationBegin = GenRandomDate(hospitalization.HospitalizationStartTime, hospitalization.HospitalizationEndTime);
                    var dateOfExaminationEnd = dateOfExaminationBegin.AddHours(_rnd.Next(1, 6));

                    var usedDevices = new List<string>();
                    var usedDevicesCount = _rnd.Next(1, 3);

                    for (var j = 0; j < usedDevicesCount; j++)
                    {
                        usedDevices.Add(UsedDevices[_rnd.Next(0, UsedDevices.Count - 1)]);
                    }

                    var sickLeave = _rnd.NextDouble() < 0.5;

                    var examination = new Examination
                    {
                        ExaminationId = Guid.NewGuid(),
                        HospitalizationId = hospitalization.HospitalizationId,
                        ExaminationStartTime = dateOfExaminationBegin,
                        ExaminationEndTime = dateOfExaminationEnd,
                        UsedDevices = string.Join(",", usedDevices),
                        Examinator = $"{FirstPersonelNames[_rnd.Next(0, FirstPersonelNames.Count - 1)]} {LastPersonelNames[_rnd.Next(0, LastPersonelNames.Count - 1)]}",
                        PrivateVisit = _rnd.NextDouble() < 0.5,
                        SignedReceip = _rnd.NextDouble() < 0.5,
                        SickLeave = sickLeave,
                        SickLeaveFrom = sickLeave ? GenRandomDate(dateOfExaminationBegin.AddDays(-3), dateOfExaminationEnd.AddDays(-1)) : (DateTime?)null,
                        SickLeaveTo = sickLeave ? GenRandomDate(dateOfExaminationBegin.AddDays(7), dateOfExaminationEnd.AddDays(14)) : (DateTime?)null,
                        ExaminationDetails = Guid.NewGuid().ToString()
                    };

                    Console.WriteLine($"Adding Examination to Hospitalization {hospitalization.HospitalizationId}");
                    _examinations.Add(examination);
                    examinationRepository.Create(examination);
                    Console.WriteLine($"Entity {examination.ExaminationId} created");
                }
            }
        }

        private void AddTreatments()
        {
            var treatmentRepository = ObjectBuilder.Container.GetInstance<IRepository<Treatment>>();

            foreach (var hospitalization in _hospitalizations)
            {
                var takeCount = _rnd.Next(0, 3);
                for (var i = 0; i < takeCount; i++)
                {
                    var treatmentStartDate = GenRandomDate(hospitalization.HospitalizationStartTime, hospitalization.HospitalizationEndTime);
                    var treatmentEndDate = treatmentStartDate.AddHours(_rnd.Next(3,18));
                    var personel = new List<string>();
                    var usedDevices = new List<string>();
                    var personelCount = _rnd.Next(1, 3);
                    var usedDevicesCount = _rnd.Next(1, 3);
                    for (var j = 0; j < personelCount; j++)
                    {
                        personel.Add($"{FirstPersonelNames[_rnd.Next(0,FirstPersonelNames.Count-1)]} {LastPersonelNames[_rnd.Next(0, LastPersonelNames.Count-1)]}");
                    }

                    for (var j = 0; j < usedDevicesCount; j++)
                    {
                        usedDevices.Add(UsedDevices[_rnd.Next(0, UsedDevices.Count-1)]);
                    }


                    var treatment = new Treatment
                    {
                        TreatmentId = Guid.NewGuid(),
                        HospitalizationId = hospitalization.HospitalizationId,
                        Personel = string.Join(",", personel),
                        UsedDevices = string.Join(",", usedDevices),
                        TreatmentStartDate = treatmentStartDate,
                        TreatmentEndDate = treatmentEndDate
                    };

                    Console.WriteLine($"Adding Treatment to Hospitalization {hospitalization.HospitalizationId}");
                    _treatments.Add(treatment);
                    treatmentRepository.Create(treatment);
                    Console.WriteLine($"Entity {treatment.TreatmentId} saved.");
                }

            }
        }

        private void AddUsedMedicines()
        {
            var usedMedicineRepository = ObjectBuilder.Container.GetInstance<IRepository<UsedMedicine>>();
            foreach (var treatment in _treatments)
            {
                var takeCount = _rnd.Next(3, 10);
                for (var i = 0; i < takeCount; i++)
                {
                    var usedMedicine = new UsedMedicine
                    {
                        UsedMedicineId = Guid.NewGuid(),
                        TreatmentId = treatment.TreatmentId,
                        MedicineId = _medicines[_rnd.Next(0, _medicines.Count - 1)].MedicineId,
                        Dose = GetRandomNumber(1.0, 1000.0)
                    };

                    Console.WriteLine($"Adding Medicine to Treatment {treatment.TreatmentId}");
                    _usedMedicines.Add(usedMedicine);
                    usedMedicineRepository.Create(usedMedicine);
                    Console.WriteLine($"Entity {usedMedicine.MedicineId} saved.");
                }
            }
        }

        private DateTime GenRandomDate(DateTime min)
        {
            return GenRandomDate(min, DateTime.Now.AddMinutes(1));
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
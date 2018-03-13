using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Domain.Residence;

namespace InstitutionService.Host.Code.Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    internal class InstitutionService : IInstitutionService
    {
        private IRepository<Patient> GetPatientRepository()
        {
            return ObjectBuilder.Container.GetInstance<IRepository<Patient>>();
        }

        public bool Ping()
        {
            return true;
        }

        public string GetInstitutionName()
        {
            return ConfigurationProvider.Instance.GetInstitutionName();
        }

        public List<PatientTransferObject> GetAllPatients()
        {
            Console.WriteLine("Pobrana lista wszystkich pacjentow");
            var patients = GetPatientRepository().Entities.ToList();
            return patients.Select(SignWithInstitution).ToList();
        }

        public PatientTransferObject GetPatientInfo(string pesel)
        {
            Console.WriteLine("Pobranie informacji o konkretnym pacjencie");
            var patient = GetPatientRepository().Entities.FirstOrDefault(p => p.Pesel == pesel);
            return SignWithInstitution(patient);
        }

        public HospitalizationTransferObject GetHospitalization(Guid hospitalizationId)
        {
            Console.WriteLine("Pobranie informacji o konkretnej hospitalizacji");
            var hospitalizationRepository = ObjectBuilder.Container.GetInstance<IRepository<Hospitalization>>();
            var hospitalization =
                hospitalizationRepository.Read(hospitalizationId);
            return hospitalization == null
                ? new HospitalizationTransferObject()
                : Mapper.Map<HospitalizationTransferObject>(hospitalization);
        }

        public ExaminationTransferObject GetExamination(Guid examinationId)
        {
            Console.WriteLine("Pobranie informacji o konkretnym badaniu");
            var examinationRepository = ObjectBuilder.Container.GetInstance<IRepository<Examination>>();
            var examination = examinationRepository.Read(examinationId);
            return examination == null
                ? new ExaminationTransferObject()
                : Mapper.Map<ExaminationTransferObject>(examination);
        }

        public TreatmentTransferObject GetTreatment(Guid treatmentId)
        {
            Console.WriteLine("Pobranie informacji o konkretnym leczeniu");
            var treatmentRepository = ObjectBuilder.Container.GetInstance<IRepository<Treatment>>();
            var treatment = treatmentRepository.Read(treatmentId);
            return treatment == null ? new TreatmentTransferObject() : Mapper.Map<TreatmentTransferObject>(treatment);
        }

        public PatientHistoryTransferObject GetPatientFullHistory(string pesel)
        {
            var patient = GetPatientRepository().Entities.FirstOrDefault(p => p.Pesel == pesel);
            if(patient == null)
                return new PatientHistoryTransferObject();

            var patientHistory = new PatientHistoryTransferObject
            {
                Pesel = patient.Pesel
            };

            foreach (var hospitalization in patient.Hospitalizations)
            {
                var hospitalizationHistoryInfo = new HospitalizationHistoryTransferObject
                {
                    HospitalizationId = hospitalization.HospitalizationId,
                    HospitalizationStartTime = hospitalization.HospitalizationStartTime,
                    HospitalizationEndTime = hospitalization.HospitalizationEndTime,
                    Examinations = Mapper.Map<List<ExaminationTransferObject>>(hospitalization.Examinations),
                    Treatments = Mapper.Map<List<TreatmentTransferObject>>(hospitalization.Treatments)
                };

                patientHistory.Hospitalizations.Add(hospitalizationHistoryInfo);
            }

            return patientHistory;
        }

        private PatientTransferObject SignWithInstitution(Patient patientObject)
        {
            if (patientObject == null)
                return new PatientTransferObject {InstitutionName = GetInstitutionName()};

            var patientTransferObject = Mapper.Map<PatientTransferObject>(patientObject);
            patientTransferObject.InstitutionName = GetInstitutionName();

            return patientTransferObject;
        }
    }
}
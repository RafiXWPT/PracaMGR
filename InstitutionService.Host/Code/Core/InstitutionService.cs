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
            return patients.Select(p => ToTransferObject(p, false)).ToList();
        }

        public PatientTransferObject GetPatient(string pesel, bool fullHistory)
        {
            Console.WriteLine("Pobranie informacji o konkretnym pacjencie");
            var patient = GetPatientRepository().Entities.FirstOrDefault(p => p.Pesel == pesel);
            return ToTransferObject(patient, fullHistory);
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

        private PatientTransferObject ToTransferObject(Patient patientObject, bool fullHistory)
        {
            if(patientObject == null)
                return new PatientTransferObject();

            var transferObject = Mapper.Map<PatientTransferObject>(patientObject);
            if (!fullHistory)
            {
                transferObject.Hospitalizations.ForEach(h =>
                {
                    h.Examinations.Clear();
                    h.Treatments.Clear();
                });
            }

            return transferObject;
        }

        public HospitalizationDocumentTransferObject GetDocument(Guid hospitalizationDocumentId)
        {
            Console.WriteLine("Pobieranie zawartości pliku");
            var documentRepository = ObjectBuilder.Container.GetInstance<IRepository<HospitalizationDocument>>();
            var document = documentRepository.Read(hospitalizationDocumentId);
            return document == null
                ? new HospitalizationDocumentTransferObject()
                : Mapper.Map<HospitalizationDocumentTransferObject>(document);
        }
    }
}
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
        public string GetInstitutionName()
        {
            return ConfigurationProvider.Instance.GetInstitutionName();
        }

        public List<PatientTransferObject> GetAllPatients()
        {
            Console.WriteLine("Pobrana lista wszystkich pacjentow");
            var patients = ObjectBuilder.Container.GetInstance<IPatientRepository>().Patients.ToList();

            return patients.Select(SignWithInstitution).ToList();
        }

        public PatientTransferObject GetPatientInfo(string pesel)
        {
            Console.WriteLine("Pobranie informacji o konkretnym pacjencie");
            var patientRepository = ObjectBuilder.Container.GetInstance<IPatientRepository>();
            var patient = patientRepository.Patients.FirstOrDefault(p => p.Pesel == pesel);
            return SignWithInstitution(patient);
        }

        public HospitalizationTransferObject GetHospitalization(Guid hospitalizationId)
        {
            Console.WriteLine("Pobranie informacji o konkretnej hospitalizacji");
            var hospitalizationRepository = ObjectBuilder.Container.GetInstance<IHospitalizationRepository>();
            var hospitalization =
                hospitalizationRepository.Hospitalizations.FirstOrDefault(
                    h => h.HospitalizationId == hospitalizationId);
            return hospitalization == null
                ? new HospitalizationTransferObject()
                : Mapper.Map<HospitalizationTransferObject>(hospitalization);
        }

        public ExaminationTransferObject GetExamination(Guid examinationId)
        {
            Console.WriteLine("Pobranie informacji o konkretnym badaniu");
            var examinationRepository = ObjectBuilder.Container.GetInstance<IExaminationRepository>();
            var examination = examinationRepository.Examinations.FirstOrDefault(e => e.ExaminationId == examinationId);
            return examination == null
                ? new ExaminationTransferObject()
                : Mapper.Map<ExaminationTransferObject>(examination);
        }

        public TreatmentTransferObject GetTreatment(Guid treatmentId)
        {
            Console.WriteLine("Pobranie informacji o konkretnym leczeniu");
            var treatmentRepository = ObjectBuilder.Container.GetInstance<ITreatmentRepository>();
            var treatment = treatmentRepository.Treatments.FirstOrDefault(e => e.TreatmentId == treatmentId);
            return treatment == null ? new TreatmentTransferObject() : Mapper.Map<TreatmentTransferObject>(treatment);
        }

        public PatientHistoryTransferObject GetPatientFullHistory(string pesel)
        {
            var patientBasicInfo = GetPatientInfo(pesel);
            var patientHistory = new PatientHistoryTransferObject
            {
                Pesel = patientBasicInfo.Pesel
            };

            foreach (var hospitalization in patientBasicInfo.Hospitalizations)
            {
                var hospitalizationInfo = GetHospitalization(hospitalization.HospitalizationId);

                var hospitalizationHistoryInfo = new HospitalizationHistoryTransferObject
                {
                    HospitalizationStartTime = hospitalization.HospitalizationStartTime,
                    HospitalizationEndTime = hospitalization.HospitalizationEndTime
                };

                foreach (var examination in hospitalizationInfo.Examinations)
                {
                    var examinationInfo = GetExamination(examination.ExaminationId);
                    hospitalizationHistoryInfo.Examinations.Add(examinationInfo);
                }

                foreach (var treatment in hospitalizationInfo.Treatments)
                {
                    var treatmentInfo = GetTreatment(treatment.TreatmentId);
                    hospitalizationHistoryInfo.Treatments.Add(treatmentInfo);
                }

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
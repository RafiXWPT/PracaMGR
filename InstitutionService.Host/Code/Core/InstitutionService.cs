using System;
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

        public PatientTransferObject GetPatientInfo(string pesel)
        {
            var patientRepository = ObjectBuilder.Container.GetInstance<IPatientRepository>();
            var patient = patientRepository.Patients.FirstOrDefault(p => p.Pesel == pesel);
            return SignWithInstitution(patient);
        }

        public HospitalizationTransferObject GetHospitalization(Guid hospitalizationId)
        {
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
            var examinationRepository = ObjectBuilder.Container.GetInstance<IExaminationRepository>();
            var examination = examinationRepository.Examinations.FirstOrDefault(e => e.ExaminationId == examinationId);
            return examination == null
                ? new ExaminationTransferObject()
                : Mapper.Map<ExaminationTransferObject>(examination);
        }

        public TreatmentTransferObject GetTreatment(Guid treatmentId)
        {
            var treatmentRepository = ObjectBuilder.Container.GetInstance<ITreatmentRepository>();
            var treatment = treatmentRepository.Treatments.FirstOrDefault(e => e.TreatmentId == treatmentId);
            return treatment == null ? new TreatmentTransferObject() : Mapper.Map<TreatmentTransferObject>(treatment);
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
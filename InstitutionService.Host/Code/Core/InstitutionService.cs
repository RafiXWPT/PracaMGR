using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Domain.Residence;

namespace InstitutionService.Host.Code.Core
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class InstitutionService : IInstitutionService
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

            var hospitalization = hospitalizationRepository.Hospitalizations.FirstOrDefault(h => h.HospitalizationId == hospitalizationId);

            return hospitalization == null ? new HospitalizationTransferObject() : Mapper.Map<HospitalizationTransferObject>(hospitalization);
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

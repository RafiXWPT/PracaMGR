using System;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
using Domain;
using Domain.Interfaces;

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

            var patient = patientRepository?.Patients.FirstOrDefault(p => p.Pesel == pesel);
            return SignWithInstitution(patient);
        }

        private PatientTransferObject SignWithInstitution(Patient patientObject)
        {
            if (patientObject == null)
            {
                return new PatientTransferObject {InstitutionName = GetInstitutionName()};
            }

            var patientTransferObject = Mapper.Map<PatientTransferObject>(patientObject);
            patientTransferObject.InstitutionName = GetInstitutionName();
            return patientTransferObject;
        }
    }
}

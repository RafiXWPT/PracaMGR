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

        public PatientTransferObject GetPatientBasicInfo(string pesel)
        {
            var patientRepository = ObjectBuilder.Container.GetInstance<IPatientRepository>();

            var patient = patientRepository?.Patients.AsNoTracking().FirstOrDefault(p => p.Pesel == pesel);
            if (patient == null)
                return new PatientTransferObject();

            patient.Hospitalizations.Clear();
            return Mapper.Map<PatientTransferObject>(patient);
        }

        public PatientTransferObject GetPatientFullInfo(string pesel)
        {
            var patientRepository = ObjectBuilder.Container.GetInstance<IPatientRepository>();

            var patient = patientRepository?.Patients.FirstOrDefault(p => p.Pesel == pesel);
            return patient == null ? new PatientTransferObject() : Mapper.Map<PatientTransferObject>(patient);
        }
    }
}

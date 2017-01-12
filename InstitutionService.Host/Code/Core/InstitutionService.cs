using System;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.Core
{
    class InstitutionService : IInstitutionService
    {
        public string GetInstitutionName()
        {
            return ConfigurationProvider.Instance.GetInstitutionName();
        }

        public Patient GetPatientBasicInfo(string pesel)
        {
            var context = ObjectBuilder.Container.GetInstance<IDatabaseContext>() as InstitutionServiceDatabaseContext;

            var patient = context?.Patients.AsNoTracking().FirstOrDefault(p => p.Pesel == pesel);
            if (patient == null)
                return new Patient();

            patient.Hospitalizations.Clear();
            return Mapper.Map<Patient>(patient);
        }

        public Patient GetPatientFullInfo(string pesel)
        {
            var context = ObjectBuilder.Container.GetInstance<IDatabaseContext>() as InstitutionServiceDatabaseContext;

            var patient = context?.Patients.FirstOrDefault(p => p.Pesel == pesel);
            return patient == null ? new Patient() : Mapper.Map<Patient>(patient);
        }
    }
}

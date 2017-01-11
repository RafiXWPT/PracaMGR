using System;
using Domain;

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
            throw new NotImplementedException();
        }
    }
}

using System.ServiceModel;
using Domain;
using Domain.Residence;
using System;

namespace InstitutionService
{
    [ServiceContract]
    public interface IInstitutionService
    {
        [OperationContract]
        string GetInstitutionName();
        [OperationContract]
        PatientTransferObject GetPatientInfo(string pesel);
        [OperationContract]
        HospitalizationTransferObject GetHospitalization(Guid hospitalizationId);
    }
}
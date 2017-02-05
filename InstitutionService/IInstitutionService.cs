using System.ServiceModel;
using Domain;

namespace InstitutionService
{
    [ServiceContract]
    public interface IInstitutionService
    {
        [OperationContract]
        string GetInstitutionName();
        [OperationContract]
        PatientTransferObject GetPatientInfo(string pesel);
    }
}
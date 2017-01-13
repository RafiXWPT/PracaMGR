using System.ServiceModel;
using System.ServiceModel.Syndication;
using Domain;

namespace InstitutionService
{
    [ServiceContract]
    public interface IInstitutionService
    {
        [OperationContract]
        string GetInstitutionName();
        [OperationContract]
        PatientTransferObject GetPatientBasicInfo(string pesel);
        [OperationContract]
        PatientTransferObject GetPatientFullInfo(string pesel);
    }
}
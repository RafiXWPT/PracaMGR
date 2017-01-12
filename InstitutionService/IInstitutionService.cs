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
        Patient GetPatientBasicInfo(string pesel);
        [OperationContract]
        Patient GetPatientFullInfo(string pesel);
    }
}
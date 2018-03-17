using System.ServiceModel;
using Domain;

namespace InstitutionService
{
    [ServiceContract]
    public interface IPersonInfoService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        PersonTransferObject GetPersonInfo(string pesel);
    }
}
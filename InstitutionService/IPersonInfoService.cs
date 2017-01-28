using System.ServiceModel;
using Domain;

namespace InstitutionService
{
    [ServiceContract]
    public interface IPersonInfoService
    {
        [OperationContract]
        PersonTransferObject GetPersonInfo(string pesel);
    }
}
using System;
using System.Collections.Generic;
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

        [OperationContract]
        List<PersonTransferObject> FilterPersons(string pesel = null, string firstName = null, string lastName = null, string insuranceId = null, DateTime? birthDate = null);
    }
}
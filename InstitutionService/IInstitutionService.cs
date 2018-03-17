using System;
using System.Collections.Generic;
using System.ServiceModel;
using Domain;
using Domain.Residence;

namespace InstitutionService
{
    [ServiceContract]
    public interface IInstitutionService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        string GetInstitutionName();

        [OperationContract]
        List<PatientTransferObject> GetAllPatients();

        [OperationContract]
        PatientTransferObject GetPatient(string pesel, bool fullHistory);

        [OperationContract]
        HospitalizationTransferObject GetHospitalization(Guid hospitalizationId);

        [OperationContract]
        ExaminationTransferObject GetExamination(Guid examinationId);

        [OperationContract]
        TreatmentTransferObject GetTreatment(Guid treatmentId);
    }
}
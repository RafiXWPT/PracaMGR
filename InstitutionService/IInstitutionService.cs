using System;
using System.ServiceModel;
using Domain;
using Domain.Residence;

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

        [OperationContract]
        ExaminationTransferObject GetExamination(Guid examinationId);

        [OperationContract]
        TreatmentTransferObject GetTreatment(Guid treatmentId);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService;

namespace WebsiteApplication.CodeBehind
{
    internal class WcfDataFetcher : IDisposable
    {
        private readonly IInstitutionRepository _repository;
        private IInstitutionService _connection;

        public WcfDataFetcher(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            CloseConnection(_connection);
        }

        private static IInstitutionService EstablishConnection(string urlEndpoint)
        {
            var binding = new NetTcpBinding();
            var channel = new ChannelFactory<IInstitutionService>(binding, urlEndpoint);

            IInstitutionService client;
            try
            {
                client = channel.CreateChannel();
            }
            catch (Exception)
            {
                return null;
            }

            return client;
        }

        private void CloseConnection(IInstitutionService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        public List<PatientTransferObject> GetAllPatientsFromInstitution(Guid institutionId)
        {
            var firstOrDefault = _repository.Institutions.FirstOrDefault(i => i.InstitutionId == institutionId);
            if (firstOrDefault != null)
                _connection = EstablishConnection(firstOrDefault.InstitutionEndpointAddress);

            return _connection?.GetAllPatients();
        }

        public List<PatientTransferObject> GetPatientHistory(string pesel)
        {
            if (!_repository.Institutions.Any())
                return null;

            var patientRecords = new List<PatientTransferObject>();
            Parallel.ForEach(_repository.Institutions, institution =>
            {
                var patientHistory = GetPatientHistory(pesel, institution);
                if (patientHistory != null)
                    patientRecords.Add(patientHistory);
            });

            return patientRecords;
        }

        private PatientTransferObject GetPatientHistory(string pesel, Institution institution)
        {
            _connection = EstablishConnection(institution.InstitutionEndpointAddress);

            if (_connection == null)
                return null;

            var patient = _connection.GetPatientInfo(pesel);
            if (patient != null)
                patient.InstitutionId = institution.InstitutionId;

            return patient;
        }

        public HospitalizationTransferObject GetHospitalization(Guid hospitalizationId, string endpoint)
        {
            _connection = EstablishConnection(endpoint);

            var hospitalization = _connection?.GetHospitalization(hospitalizationId);

            return hospitalization;
        }

        public ExaminationTransferObject GetExamination(Guid examinationId, string endpoint)
        {
            _connection = EstablishConnection(endpoint);

            var examination = _connection?.GetExamination(examinationId);

            return examination;
        }

        public TreatmentTransferObject GetTreatment(Guid treatmentId, string endpoint)
        {
            _connection = EstablishConnection(endpoint);

            var treatment = _connection?.GetTreatment(treatmentId);

            return treatment;
        }
    }
}
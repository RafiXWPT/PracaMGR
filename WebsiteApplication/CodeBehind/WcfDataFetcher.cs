using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web.WebSockets;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService;
using WebsiteApplication.Models.ViewModels.Patient;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.CodeBehind
{
    internal class WcfDataFetcher
    {
        private IInstitutionService EstablishConnection(string urlEndpoint)
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

        private readonly IInstitutionRepository _repository;

        public WcfDataFetcher(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public List<PatientTransferObject> GetPatientHistory(string pesel)
        {
            if (!_repository.Institutions.Any())
                return null;

            var patientRecords = new List<PatientTransferObject>();
            foreach (var institution in _repository.Institutions)
            {
                patientRecords.Add(GetPatientHistory(pesel, institution));
            }
       
            return patientRecords;
        }

        private PatientTransferObject GetPatientHistory(string pesel, Institution institution)
        {
            var connection = EstablishConnection(institution.InstitutionEndpointAddress);
            var patient = connection.GetPatientInfo(pesel);
            CloseConnection(connection);
            return patient;
        }
    }
}

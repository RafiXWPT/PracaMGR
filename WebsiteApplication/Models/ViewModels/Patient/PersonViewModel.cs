﻿using System;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PersonViewModel
    {
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string InsuranceId { get; set; }
        public string AddressCountry { get; set; }
        public string AddressProvince { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressHomeNumber { get; set; }
    }
}
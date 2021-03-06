﻿using AutoMapper;
using Domain;
using Domain.Inventory;
using Domain.Residence;

namespace InstitutionService.Host.Code.Core
{
    public static class AutoMapperBuilder
    {
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Patient, PatientTransferObject>();
                cfg.CreateMap<Hospitalization, HospitalizationTransferObject>();
                cfg.CreateMap<HospitalizationDocument, HospitalizationDocumentTransferObject>();
                cfg.CreateMap<Examination, ExaminationTransferObject>();
                cfg.CreateMap<Treatment, TreatmentTransferObject>();
                cfg.CreateMap<Medicine, MedicineTransferObject>();
                cfg.CreateMap<UsedMedicine, UsedMedicineTransferObject>();
            });
        }
    }
}
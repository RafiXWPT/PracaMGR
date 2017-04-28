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
                cfg.CreateMap<Hospitalization, HospitalizationBasicTransferObject>();
                cfg.CreateMap<Hospitalization, HospitalizationTransferObject>();
                cfg.CreateMap<Examination, ExaminationBasicTransferObject>();
                cfg.CreateMap<Examination, ExaminationTransferObject>();
                cfg.CreateMap<Treatment, TreatmentBasicTransferObject>();
                cfg.CreateMap<Treatment, TreatmentTransferObject>();
                cfg.CreateMap<Medicine, MedicineTransferObject>();
                cfg.CreateMap<UsedMedicine, UsedMedicineTransferObject>();
            });
        }
    }
}
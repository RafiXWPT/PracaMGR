using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Hospitalization;
using Domain.Inventory;

namespace InstitutionService.Host.Code.Core
{
    public static class AutoMapperBuilder
    {
        public static void RegisterAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                /* INITIALIZE BASE ENTITY <-> ENTITY Converter */
                cfg.CreateMap<Patient, Patient>();
                cfg.CreateMap<Address, Address>();
                cfg.CreateMap<Hospitalization, Hospitalization>()
                    .ForMember(x => x.Patient, opt => opt.Ignore());
                cfg.CreateMap<Examination, Examination>()
                    .ForMember(x => x.Hospitalization, opt => opt.Ignore());
                cfg.CreateMap<Treatment, Treatment>()
                    .ForMember(x => x.Hospitalization, opt => opt.Ignore());
                cfg.CreateMap<Medicine, Medicine>();
                cfg.CreateMap<UsedMedicine, UsedMedicine>()
                    .ForMember(x => x.Treatment, opt => opt.Ignore());
            });
        }
    }
}

﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Domain.Inventory;
using Domain.Residence;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.Models.ViewModels.Institution;
using WebsiteApplication.Models.ViewModels.Patient;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;
using WebsiteApplication.Models.ViewModels.Reports;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                // CORE
                cfg.CreateMap<Institution, InstitutionViewModel>();
                cfg.CreateMap<Right, RightViewModel>();
                cfg.CreateMap<IdentityRole, RoleViewModel>();
                // DTO OBJECTS
                cfg.CreateMap<Institution, InstitutionTransferObject>();
                // VIEW MODELS
                cfg.CreateMap<ReportRequest, ReportRequestViewModel>();
                cfg.CreateMap<PersonTransferObject, PersonViewModel>();
                cfg.CreateMap<PatientTransferObject, PatientViewModel>();
                cfg.CreateMap<HospitalizationTransferObject, HospitalizationContainerViewModel>();
                cfg.CreateMap<HospitalizationTransferObject, HospitalizationViewModel>();
                cfg.CreateMap<ExaminationTransferObject, ExaminationContainerViewModel>();
                cfg.CreateMap<TreatmentTransferObject, TreatmentContainerViewModel>();
                cfg.CreateMap<ExaminationTransferObject, ExaminationViewModel>();
                cfg.CreateMap<TreatmentTransferObject, TreatmentViewModel>();
                cfg.CreateMap<UsedMedicineTransferObject, UsedMedicineViewModel>();
                cfg.CreateMap<HospitalizationDocumentTransferObject, HospitalizationDocumentViewModel>();
            });
        }
    }
}
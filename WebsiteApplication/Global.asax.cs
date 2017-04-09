using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Domain.Residence;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using WebsiteApplication.Models.ViewModels.Patient;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

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
                // DTO OBJECTS
                cfg.CreateMap<Institution, InstitutionTransferObject>();
                // VIEW MODELS
                cfg.CreateMap<PersonTransferObject, PersonViewModel>();
                cfg.CreateMap<PatientTransferObject, PatientViewModel>();
                cfg.CreateMap<HospitalizationBasicTransferObject, HospitalizationViewModel>();
                cfg.CreateMap<HospitalizationTransferObject, HospitalizationContainerViewModel>();
                cfg.CreateMap<ExaminationBasicTransferObject, ExaminationViewModel>();
                cfg.CreateMap<ExaminationTransferObject, ExaminationContainerViewModel>();
                cfg.CreateMap<TreatmentBasicTransferObject, TreatmentViewModel>();
                cfg.CreateMap<TreatmentTransferObject, TreatmentContainerViewModel>();
            });
        }
    }
}

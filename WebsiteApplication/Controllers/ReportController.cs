using System;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Raport;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRepository<ReaportRequest> _reaportRequestRepository;
        private readonly IRaportService _raportService;

        public ReportController(IRepository<ReaportRequest> reaportRequestRepository, IRaportService raportService)
        {
            _reaportRequestRepository = reaportRequestRepository;
            _raportService = raportService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeRight(Right = "REAPORT_GENERATION")]
        public ActionResult AddReaport(string pesel)
        {
            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.PENDING
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json("OK");
        }

        [AuthorizeRight(Right = "FORCE_REAPORT_GENERATION")]
        public ActionResult GenerateReaport(string pesel)
        {
            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.ACCEPTED,
                GeneratedReaport = _raportService.GenerateRaport(pesel)
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json("OK");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult AcceptReaport(Guid reaportId)
        {
            var reaportRequest = _reaportRequestRepository.Read(reaportId);
            reaportRequest.Status = ReaportRequestStatus.ACCEPTED;
            reaportRequest.GeneratedReaport = _raportService.GenerateRaport(reaportRequest.PatientPesel);
            _reaportRequestRepository.Update(reaportRequest);
            return Json("OK");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult RejectReaport(Guid reaportId)
        {
            var reaportRequest = _reaportRequestRepository.Read(reaportId);
            reaportRequest.Status = ReaportRequestStatus.REJECTED;
            _reaportRequestRepository.Update(reaportRequest);
            return Json("OK");
        }
    }
}
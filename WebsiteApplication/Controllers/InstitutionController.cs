using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.Filters;
using WebsiteApplication.Models.ViewModels.Institution;

namespace WebsiteApplication.Controllers
{
    [RoleAuthorize(Roles = "ADMIN,ADMIN_TECH,TECHNICAN")]
    public class InstitutionController : BaseController
    {
        private readonly IInstitutionRepository _repository;

        public InstitutionController(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        // GET: Institution
        public ActionResult Index()
        {
            var institutions = _repository.Institutions.ToList();
            return View(Mapper.Map<List<Institution>, List<InstitutionViewModel>>(institutions));
        }

        // GET: Institution/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var institution = _repository.Institutions.First(x => x.InstitutionId == id);
            if (institution == null)
                return HttpNotFound();

            return View(Mapper.Map<InstitutionViewModel>(institution));
        }

        // GET: Institution/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Institution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "InstitutionEndpointAddress,InstitutionName,Address")] InstitutionViewModel institution)
        {
            if (ModelState.IsValid)
            {
                var newInstitution = new Institution();
                newInstitution.InstitutionName = institution.InstitutionName;
                newInstitution.InstitutionEndpointAddress = institution.InstitutionEndpointAddress;
                newInstitution.Address = institution.Address;
                newInstitution.InstitutionId = Guid.NewGuid();
                newInstitution.Address.AddressId = Guid.NewGuid();
                _repository.Add(newInstitution);
                return RedirectToAction("Index");
            }

            return View(institution);
        }

        // GET: Institution/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var institution = _repository.Institutions.First(x => x.InstitutionId == id);
            if (institution == null)
                return HttpNotFound();
            return View(Mapper.Map<InstitutionViewModel>(institution));
        }

        // POST: Institution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "InstitutionId,InstitutionEndpointAddress")] InstitutionViewModel institution)
        {
            if (ModelState.IsValid)
            {
                var repositoryInstitution =
                    _repository.Institutions.FirstOrDefault(i => i.InstitutionId == institution.InstitutionId);

                if (repositoryInstitution == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                repositoryInstitution.InstitutionName = institution.InstitutionName;
                repositoryInstitution.InstitutionEndpointAddress = institution.InstitutionEndpointAddress;
                repositoryInstitution.Address = institution.Address;

                _repository.Update(repositoryInstitution);
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        // GET: Institution/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var institution = _repository.Institutions.First(x => x.InstitutionId == id);

            if (institution == null)
                return HttpNotFound();
            return View(Mapper.Map<InstitutionViewModel>(institution));
        }

        // POST: Institution/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var institution = _repository.Institutions.First(x => x.InstitutionId == id);
            _repository.Delete(institution);
            return RedirectToAction("Index");
        }
    }
}
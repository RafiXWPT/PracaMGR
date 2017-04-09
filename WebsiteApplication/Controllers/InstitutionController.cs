using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;

namespace WebsiteApplication.Controllers
{
    public class InstitutionController : CultureController
    {
        private readonly IInstitutionRepository _repository;

        public InstitutionController(IInstitutionRepository repository)
        {
            _repository = repository;
        }
        // GET: Institution
        public ActionResult Index()
        {
            return View(_repository.Institutions.ToList());
        }

        // GET: Institution/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _repository.Institutions.First(x => x.InstitutionId == id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
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
        public ActionResult Create([Bind(Include = "InstitutionEndpointAddress,InstitutionName,Address")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                institution.InstitutionId = Guid.NewGuid();
                institution.Address.AddressId = Guid.NewGuid();
                _repository.Add(institution);
                return RedirectToAction("Index");
            }

            return View(institution);
        }

        // GET: Institution/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _repository.Institutions.First(x => x.InstitutionId == id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstitutionId,InstitutionEndpointAddress")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(institution);
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        // GET: Institution/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = _repository.Institutions.First(x => x.InstitutionId == id);

            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Institution institution = _repository.Institutions.First(x => x.InstitutionId == id);
            _repository.Delete(institution);
            return RedirectToAction("Index");
        }
    }
}

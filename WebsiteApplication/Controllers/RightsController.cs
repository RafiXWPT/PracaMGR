using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind;
using System.Web.Security;
using AutoMapper;
using Kendo.Mvc.Extensions;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.Controllers
{
    public class RightsController : BaseController
    {
        public IRightsManager Manager { get; }

        public RightsController(IRightsManager manager)
        {
            Manager = manager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReadRights(DataSourceRequest request)
        {
            var rights = Manager.Rights();

            return Json(Mapper.Map<List<RightViewModel>>(rights).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateRight(DataSourceRequest request, RightViewModel viewModel)
        {
            var newRight = Manager.AddRight(viewModel.RightName, viewModel.RightDescription);
            if (newRight != null)
            {
                viewModel.Id = newRight.Id;
                return Json(new[] { newRight }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }

        public ActionResult UpdateRight(DataSourceRequest request, RightViewModel viewModel)
        {
            var editedRight = Manager.EditRight(viewModel.Id, viewModel.RightName, viewModel.RightDescription);
            return Json(new[] { editedRight }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DestroyRight(DataSourceRequest request, RightViewModel viewModel)
        {
            var removedRight = Manager.RemoveRight(viewModel.Id);
            return Json(new[] { removedRight }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
    }
}
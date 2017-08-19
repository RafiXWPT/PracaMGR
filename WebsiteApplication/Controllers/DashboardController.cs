using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models.ViewModels.Tile;

namespace WebsiteApplication.Controllers
{
    public class DashboardController : KendoController
    {
        public WebsiteDatabaseContext Context { get; }

        public DashboardController(WebsiteDatabaseContext context)
        {
            Context = context;
        }

        public ActionResult Index()
        {
            var availableActions = new List<TileViewModel>();

            if (User.Roles.Contains("REAPORT_ADMIN") || User.Roles.Contains("ADMIN"))
            {
                availableActions.Add(new TileViewModel {Break = true, BreakHeader = "ADMINISTRACJA"});
                availableActions.Add(new TileViewModel
                {
                    TileColor = "dark-blue",
                    TileIcon = "address-card",
                    TileContentText = "role i uprawnienia",
                    TileUrl = Url.Action("Index", "Rights"),
                });
            }
            if (User.Rights.Contains("APPLICATION_INFO"))
            {
                availableActions.Add(new TileViewModel {Break = true, BreakHeader = "APLIKACJA"});
                availableActions.Add(new TileViewModel
                {
                    TileColor = "orange",
                    TileIcon = "university",
                    TileValue = Context.Institutions.Count().ToString(),
                    TileContentText = "instytucje",
                });
                availableActions.Add(new TileViewModel
                {
                    TileColor = "orange",
                    TileIcon = "check",
                    TileValue = Context.ReaportRequests.Count(r => r.Status == ReaportRequestStatus.ACCEPTED).ToString(),
                    TileContentText = "raporty",
                });
                availableActions.Add(new TileViewModel
                {
                    TileColor = "orange",
                    TileIcon = "ban",
                    TileValue = Context.ReaportRequests.Count(r => r.Status == ReaportRequestStatus.REJECTED).ToString(),
                    TileContentText = "raporty",
                });
                availableActions.Add(new TileViewModel
                {
                    TileColor = "orange",
                    TileIcon = "search",
                    TileValue = Context.SearchHistories.Count().ToString(),
                    TileContentText = "żądania wyszukania",
                });
            }
            if (User.Rights.Contains("REAPORT_ACCEPTANCE"))
            {
                availableActions.Add(new TileViewModel { Break = true, BreakHeader = "RAPORTY"});
                var reportsToAcceptance = Context.ReaportRequests.Count(r => r.Status == ReaportRequestStatus.PENDING);
                availableActions.Add(new TileViewModel
                {
                    TileColor = "dark-blue",
                    TileIcon = reportsToAcceptance > 0 ? "folder-open" : "folder",
                    TileValue = reportsToAcceptance.ToString(),
                    TileContentText = "raporty oczekujące",
                    TileUrl = Url.Action("Index", "ReportList")
                });
            }

            return View(availableActions);
        }
    }
}
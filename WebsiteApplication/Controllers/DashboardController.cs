using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models.ViewModels.Tile;

namespace WebsiteApplication.Controllers
{
    public class DashboardController : KendoController
    {
        public DashboardController(WebsiteDatabaseContext context)
        {
            Context = context;
        }

        public WebsiteDatabaseContext Context { get; }

        public ActionResult Index()
        {
            var availableActions = new List<TileBase>();

            if (User.Roles.Contains("REPORT_ADMIN") || User.Roles.Contains("ADMIN"))
            {
                availableActions.Add(new TileBreakViewModel {BreakHeader = "ADMINISTRACJA"});
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = "dark-blue",
                    TileContentColor = "dark-blue",
                    TileIcon = "address-card",
                    TileContentText = "role i uprawnienia",
                    TileUrl = Url.Action("Index", "Rights")
                });
            }
            if (User.Rights.Contains("APPLICATION_INFO"))
            {
                availableActions.Add(new TileBreakViewModel {BreakHeader = "APLIKACJA"});
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = "orange",
                    TileContentColor = "orange",
                    TileIcon = "university",
                    TileValue = Context.Institutions.Count().ToString(),
                    TileContentText = "instytucje"
                });
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = "orange",
                    TileContentColor = "orange",
                    TileIcon = "check",
                    TileValue =
                        Context.ReaportRequests.Count(r => r.Status == ReportRequestStatus.ACCEPTED).ToString(),
                    TileContentText = "raporty"
                });
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = "orange",
                    TileContentColor = "orange",
                    TileIcon = "ban",
                    TileValue =
                        Context.ReaportRequests.Count(r => r.Status == ReportRequestStatus.REJECTED).ToString(),
                    TileContentText = "raporty"
                });
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = "orange",
                    TileContentColor = "orange",
                    TileIcon = "search",
                    TileValue = Context.SearchHistories.Count().ToString(),
                    TileContentText = "żądania wyszukania"
                });
            }
            if (User.Rights.Contains("REPORT_ACCEPTANCE"))
            {
                availableActions.Add(new TileBreakViewModel {BreakHeader = "RAPORTY"});
                var reportsToAcceptance = Context.ReaportRequests.Count(r => r.Status == ReportRequestStatus.PENDING);
                availableActions.Add(new TileViewModel
                {
                    TileCircleColor = reportsToAcceptance > 0 ? "green" : "dark-blue",
                    TileContentColor = "dark-blue",
                    TileIcon = reportsToAcceptance > 0 ? "folder-open" : "folder",
                    TileValue = reportsToAcceptance.ToString(),
                    TileContentText = "raporty oczekujące",
                    TileUrl = Url.Action("ReportsList", "ReportsList")
                });
            }

            return View(availableActions);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class KendoController : BaseController
    {
        protected DataSourceResult DataSourceResult<TEntity, TViewModel>(DataSourceRequest request, IEnumerable<TEntity> entities)
        {
            return entities.ToDataSourceResult(request, Mapper.Map<TEntity, TViewModel>);
        }

        protected ActionResult JsonDataSourceResult<TEntity, TViewModel>(DataSourceRequest request, IEnumerable<TEntity> entities)
        {
            return Json(DataSourceResult<TEntity, TViewModel>(request, entities), JsonRequestBehavior.AllowGet);
        }
    }
}
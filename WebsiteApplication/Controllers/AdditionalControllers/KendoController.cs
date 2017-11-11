using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class KendoController : BaseController
    {
        protected DataSourceResult DataSourceResult<TEntity, TViewModel>(DataSourceRequest request,
            IEnumerable<TEntity> entities)
            where TEntity : class
            where TViewModel : class
        {
            return entities.ToDataSourceResult(request, Mapper.Map<TEntity, TViewModel>);
        }

        protected ActionResult JsonDataSourceResult<TSource>(DataSourceRequest request, TSource entity)
        {
            return Json(new[] {entity}.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult JsonDataSourceResult<TSource>(DataSourceRequest request, IEnumerable<TSource> entities)
        {
            return Json(entities.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult JsonDataSourceResult<TSource>(DataSourceRequest request, ModelStateDictionary modelState,
            TSource entity)
        {
            return Json(new[] {entity}.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult JsonDataSourceResult<TSource>(DataSourceRequest request, ModelStateDictionary modelState,
            IEnumerable<TSource> entities)
        {
            return Json(entities.ToDataSourceResult(request, modelState), JsonRequestBehavior.AllowGet);
        }

        protected ActionResult JsonDataSourceResult<TEntity, TViewModel>(DataSourceRequest request,
            IEnumerable<TEntity> entities)
            where TEntity : class
            where TViewModel : class
        {
            return Json(DataSourceResult<TEntity, TViewModel>(request, entities), JsonRequestBehavior.AllowGet);
        }

        protected TViewModel MapSingle<TEntity, TViewModel>(TEntity entity)
            where TEntity : class
            where TViewModel : class
        {
            return Mapper.Map<TViewModel>(entity);
        }

        protected IEnumerable<TViewModel> MapMany<TEntity, TViewModel>(IEnumerable<TEntity> entities)
            where TEntity : class
            where TViewModel : class
        {
            return Mapper.Map<IEnumerable<TViewModel>>(entities);
        }
    }
}
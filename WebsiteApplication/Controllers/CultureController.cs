﻿using System;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind;

namespace WebsiteApplication.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        public ActionResult SetCulture(string culture)
        {
            culture = CultureHelper.GetImplementedCulture(culture);
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture")
                {
                    Value = culture,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);

            return Json("");
        }
    }
}
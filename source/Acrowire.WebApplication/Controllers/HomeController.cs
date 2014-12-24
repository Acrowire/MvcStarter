using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acrowire.WebApplication;
using Acrowire.Application;
using Newtonsoft.Json;
using System.Collections;
using Acrowire.WebApplication.Extensions;
using Acrowire.WebApplication.Controllers.Extensions;

namespace Acrowire.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Default Content
            var model = new ExpandoObject();


            return View(model);
        }

        public ActionResult Unauthroized()
        {
            return View();
        }

    }
}
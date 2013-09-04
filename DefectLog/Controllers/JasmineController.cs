using System;
using System.Web.Mvc;
using DefectLog.Areas.Admin.Controllers;

namespace DefectLog.Controllers
{
    public class JasmineController : AdminController
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}

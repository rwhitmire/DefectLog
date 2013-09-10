using System.Web.Mvc;
using DefectLog.Web.Areas.Admin.Controllers;

namespace DefectLog.Web.Controllers
{
    public class JasmineController : AdminController
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}

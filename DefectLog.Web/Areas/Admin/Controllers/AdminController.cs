using System.Web.Mvc;
using DefectLog.Web.Controllers;

namespace DefectLog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {

    }
}

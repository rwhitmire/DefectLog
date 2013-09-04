using System.Web.Mvc;
using DefectLog.Controllers;

namespace DefectLog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {

    }
}

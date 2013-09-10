using System.Web.Http;
using DefectLog.Web.Controllers;

namespace DefectLog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminApiController : BaseApiController
    {
    }
}
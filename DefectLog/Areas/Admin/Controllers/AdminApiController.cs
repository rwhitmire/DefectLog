using System.Web.Http;
using DefectLog.Controllers;

namespace DefectLog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public abstract class AdminApiController : BaseApiController
    {
    }
}
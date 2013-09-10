using System.Web.Http;

namespace DefectLog.Web.Controllers
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {

    }
}

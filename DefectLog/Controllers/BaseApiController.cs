using System.Web.Http;

namespace DefectLog.Controllers
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {

    }
}

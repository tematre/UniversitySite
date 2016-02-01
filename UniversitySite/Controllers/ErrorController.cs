using System.Web.Mvc;

namespace UniversitySite.Controllers
{
    public class ErrorController : BaseController
    {
        public ViewResult Index()
        {
            return View("Error");
        }
    }
}
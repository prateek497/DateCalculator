using System.Web.Mvc;

namespace DateCalculator.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }
    }
}

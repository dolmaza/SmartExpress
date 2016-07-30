using System.Web.Mvc;

namespace SmartExpress.Controllers
{
    public class HomeController : Controller
    {
        [Route("", Name = "Home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
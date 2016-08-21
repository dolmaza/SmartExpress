using SmartExpress.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Controllers
{
    public class HomeController : BaseController
    {
        [Route("", Name = "Home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
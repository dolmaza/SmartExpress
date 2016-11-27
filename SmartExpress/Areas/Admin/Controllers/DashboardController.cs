using SmartExpress.Admin.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        [Route("")]
        [Route("dashboard", Name = "Dashboard")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
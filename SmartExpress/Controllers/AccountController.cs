using SmartExpress.Admin.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Controllers
{
    public class AccountController : BaseController
    {
        [Route("account/login", Name = "Login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}
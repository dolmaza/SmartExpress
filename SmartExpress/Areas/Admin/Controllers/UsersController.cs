using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class UsersController : BaseController
    {
        [Route("users", Name = "Users")]
        public ActionResult Index()
        {
            var model = new UserViewModel
            {
                UserCreateUrl = Url.RouteUrl("UsersCreate")
            };
            return View(model);
        }

        [Route("users/create", Name = "UsersCreate")]
        public ActionResult CreateUser()
        {
            var model = new CreateEditUserViewModel
            {
                SaveUrl = Url.RouteUrl("UsersCreate"),
                UsersUrl = Url.RouteUrl("Users"),
                Title = "მომხმარებლის დამატება"

            };

            return View("CreateEditUser", model);
        }
    }
}
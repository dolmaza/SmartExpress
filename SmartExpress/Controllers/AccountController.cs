using Core.Utilities;
using SmartExpress.Models;
using SmartExpress.Reusable;
using SmartExpress.Reusable.Extentions;
using SmartExpress.Reusable.FilterAttributes;
using System.Web.Mvc;

namespace SmartExpress.Controllers
{
    public class AccountController : BaseController
    {
        #region Login

        [Route("account/login", Name = "Login")]
        public ActionResult Login()
        {
            var model = new LoginViewModel
            {
                SaveUrl = Url.RouteUrl("Login")
            };

            return View(model);
        }

        [HttpPost]
        [Route("account/login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = UnitOfWork.UserRepository.GetUserByContractNumber(model.ContractNumber);

            if (user.Password.VerifyPassword(model.Password))
            {
                Session[AppSettings.AuthenticatedUserKey] = user;
                if (user.Role.IntCode == 1)
                {
                    return Redirect(Url.RouteUrl("Dashboard"));
                }
                else
                {
                    return Redirect(Url.RouteUrl("UserInvoices", new { ID = user.ID }));
                }
            }

            return View(model);
        }

        [Route("account/logout", Name = "Logout")]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect(Url.RouteUrl("Home"));
        }

        #endregion

        #region Profile

        [UserAuthorize]
        [Route("account/{ID}/invoices", Name = "UserInvoices")]
        public ActionResult UserInvoices(int? ID)
        {
            return View();
        }

        #endregion
    }
}
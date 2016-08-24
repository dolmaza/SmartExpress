using Core.Utilities;
using SmartExpress.Models;
using SmartExpress.Reusable;
using SmartExpress.Reusable.Extentions;
using SmartExpress.Reusable.FilterAttributes;
using System.Linq;
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
            var model = new UserInvoicesViewModel()
            {
                UserInvoceObjects = UnitOfWork.InvoiceRepository.GetUserInvoices(ID).Select(i => new UserInvoicesViewModel.UserInvoiceObject
                {
                    ID = i.ID,
                    InvoiceNumber = i.InvoiceNumber,
                    MessageModeCaption = i.MessageMode.Caption,
                    ReceiverDate = i.ReceiveDate?.ToShortDateString(),
                    DetailsUrl = Url.RouteUrl("UserInvoiceDetails", new { ID = ID, invoiceNumber = i.InvoiceNumber })
                }).ToList()
            };
            return View(model);
        }

        [Route("account/{ID}/invoices/{invoiceNumber}details", Name = "UserInvoiceDetails")]
        public ActionResult UserInvoiceDetails(int? ID, string invoiceNumber)
        {
            var model = new UserInvoiceDetailsViewModel
            {
                UserInvoiceDetailObjects = UnitOfWork.InvoiceRepository.GetUserInvoiceDetails(ID, invoiceNumber).Select(i => new UserInvoiceDetailsViewModel.UserInvoiceDetailObject
                {
                    ID = i.ID,
                    ReceiverFirstname = i.ReceiverFirstname,
                    ReceiverLastname = i.ReceiverLastname,
                    ReceiverAddress = i.ReceiverAddress,
                    DeliveryDate = i.DeliveryDate?.ToShortDateString(),
                    WhoReceived = i.WhoReceived
                }).ToList()
            };
            return View(model);
        }

        #endregion
    }
}
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class InvoicesController : BaseController
    {
        [Route("invoices", Name = "Invoices")]
        public ActionResult Index()
        {
            var model = new InvoiceViewModel
            {
                InvoiceCreateUrl = Url.RouteUrl("InvoicesCreate")
            };
            return View(model);
        }

        [Route("invoices/create", Name = "InvoicesCreate")]
        public ActionResult CreateInvoice()
        {
            var model = new CreateEditInvoiceViewModel
            {
                SaveUrl = Url.RouteUrl("InvoicesCreate"),
                InvoicesUrl = Url.RouteUrl("Invoices"),
                Title = "ზედნადების დამატება"
            };
            return View("CreateEditInvoice", model);
        }
    }
}
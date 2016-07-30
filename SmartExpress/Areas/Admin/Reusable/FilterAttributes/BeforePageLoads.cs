using SmartExpress.Admin.Models;
using System.Web.Mvc;

namespace SmartExpress.Admin.Reusable.FilterAttributes
{
    public class BeforePageLoads : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var model = new LayoutViewModel();
            var controller = (BaseController)filterContext.Controller;

            InitMenu(ref model, controller);

            controller.ViewBag.LayoutViewModel = model;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        private void InitMenu(ref LayoutViewModel model, Controller controller)
        {
            model.HomeUrl = controller.Url.RouteUrl("Home");

            model.DashboardUrl = controller.Url.RouteUrl("Dashboard");
            model.UsersUrl = controller.Url.RouteUrl("Users");
            model.DictionariesUrl = controller.Url.RouteUrl("Dictionaries");
            model.InvoicesUrl = controller.Url.RouteUrl("Invoices");
        }
    }
}
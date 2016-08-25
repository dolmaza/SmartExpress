using Core;
using Core.Utilities;
using SmartExpress.Models;
using System.Web.Mvc;

namespace SmartExpress.Reusable.FilterAttributes
{
    public class BeforePageLoads : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var model = new LayoutViewModel();
            GetUserFromSession(filterContext);
            InitMenuItems(filterContext, ref model);

            filterContext.Controller.ViewBag.LayoutViewModel = model;
        }

        private void GetUserFromSession(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;
            var controller = (BaseController)filterContext.Controller;

            controller.UserItem = user;
        }

        private void InitMenuItems(ActionExecutingContext filterContext, ref LayoutViewModel model)
        {
            var controller = (BaseController)filterContext.Controller;
            model.LoginUrl = controller.Url.RouteUrl("Login");
            model.LogoutUrl = controller.Url.RouteUrl("Logout");
            model.HomeUrl = controller.UserItem == null ? controller.Url.RouteUrl("Home") : controller.Url.RouteUrl("UserInvoices", new { ID = controller.UserItem?.ID });
            model.DashboardUrl = controller.Url.RouteUrl("Dashboard");
            model.IsUserAuthorized = controller.UserItem != null;
            model.IsAdmin = controller.UserItem?.Role.IntCode == 1;
        }
    }
}
using Core.Utilities;
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
            GetSuccessErrorMessage(filterContext, ref model);

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

        private void GetSuccessErrorMessage(ActionExecutingContext filterContext, ref LayoutViewModel model)
        {
            if (filterContext.HttpContext.Session[AppSettings.ErrorMessageKey] != null)
            {
                model.SuccessErrorMessageInfo = new SuccessErrorMessageInfo
                {
                    Message = filterContext.HttpContext.Session[AppSettings.ErrorMessageKey].ToString()
                };
                filterContext.HttpContext.Session[AppSettings.ErrorMessageKey] = null;
            }
            else if (filterContext.HttpContext.Session[AppSettings.SuccessMessageKey] != null)
            {
                model.SuccessErrorMessageInfo = new SuccessErrorMessageInfo
                {
                    IsSuccess = true,
                    Message = filterContext.HttpContext.Session[AppSettings.SuccessMessageKey].ToString()
                };
                filterContext.HttpContext.Session[AppSettings.SuccessMessageKey] = null;
            }
        }
    }
}
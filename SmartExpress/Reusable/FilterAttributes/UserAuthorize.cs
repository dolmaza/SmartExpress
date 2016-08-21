using Core;
using Core.Utilities;
using System.Web.Mvc;

namespace SmartExpress.Reusable.FilterAttributes
{
    public class UserAuthorize : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session[AppSettings.AuthenticatedUserKey] as User;

            if (user == null)
            {
                filterContext.Result = new RedirectResult("/account/login");

            }
        }
    }
}
using Core;
using SmartExpress.Admin.Reusable.FilterAttributes;
using System.Web.Mvc;

namespace SmartExpress.Admin.Reusable
{
    [BeforePageLoads]
    public class BaseController : Controller
    {
        public User UserItem { get; set; }

    }
}
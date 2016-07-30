using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class DictionariesController : BaseController
    {
        [Route("dictionaries", Name = "Dictionaries")]
        public ActionResult Index()
        {
            var model = new DictionaryViewModel
            {

            };

            return View(model);
        }
    }
}
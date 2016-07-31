using Core;
using Core.DB;
using Core.UnitOfWork;
using SmartExpress.Admin.Reusable.FilterAttributes;
using System.Web.Mvc;

namespace SmartExpress.Admin.Reusable
{
    [BeforePageLoads]
    public class BaseController : Controller
    {
        public User UserItem { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseController()
        {
            UnitOfWork = new UnitOfWork(new DbCoreDataContext());
        }

    }

    public class BaseViewModel
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public BaseViewModel()
        {
            UnitOfWork = new UnitOfWork(new DbCoreDataContext());
        }
    }
}
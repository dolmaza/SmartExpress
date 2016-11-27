using SmartExpress.Models;
using SmartExpress.Reusable;
using SmartExpress.Reusable.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace SmartExpress.Controllers
{
    public class HomeController : BaseController
    {
        [Route("", Name = "Home")]
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                JuridicalPerson = new CourierCallJuridicalViewModel
                {
                    ServiceTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 3, true).Select(d => new SimpleKeyValueObject<int?, string>
                    {
                        Key = d.ID,
                        Value = d.Caption
                    }).ToList(),

                    MessageTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 2, true).Select(d => new SimpleKeyValueObject<int?, string>
                    {
                        Key = d.ID,
                        Value = d.Caption
                    }).ToList()
                },

                PhysicalPerson = new CourierCallPhysicalViewModel
                {
                    ServiceTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 3, true).Select(d => new SimpleKeyValueObject<int?, string>
                    {
                        Key = d.ID,
                        Value = d.Caption
                    }).ToList(),

                    MessageTypes = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 2, true).Select(d => new SimpleKeyValueObject<int?, string>
                    {
                        Key = d.ID,
                        Value = d.Caption
                    }).ToList()
                }
            };
            return View(model);
        }
    }
}
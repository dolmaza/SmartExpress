using Core;
using Core.Properties;
using DevExpress.Web.Mvc;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    public class DictionariesController : BaseController
    {
        [Route("dictionaries", Name = "Dictionaries")]
        public ActionResult Index()
        {
            var model = new DictionariesViewModel
            {
                TreeViewModel = GetDictionariesTreeViewModel()
            };

            return View(model);
        }

        [Route("dictionaries/list", Name = "DictionariesList")]
        public ActionResult DictionariesList()
        {
            return PartialView("_DictionariesTree", GetDictionariesTreeViewModel());
        }

        [HttpPost]
        [Route("dictionaries/create", Name = "DictionariesCreate")]
        public ActionResult CreateDictionary([ModelBinder(typeof(DevExpressEditorsBinder))] DictionaryTreeItem model)
        {
            UnitOfWork.DictionaryRepository.Add(new Dictionary
            {
                ParentID = model.ParentID,
                Caption = model.Caption,
                IntCode = model.IntCode,
                StringCode = model.StringCode,
                DictionaryCode = model.DictionaryCode,
                SortVal = model.SortVal,
                IsVisible = model.IsVisible,
                CreateTime = DateTime.Now
            });

            if (UnitOfWork.DictionaryRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionariesTree", GetDictionariesTreeViewModel());
        }

        [HttpPost]
        [Route("dictionaries/update", Name = "DictionariesUpdate")]
        public ActionResult EditDictionary([ModelBinder(typeof(DevExpressEditorsBinder))] DictionaryTreeItem model)
        {

            UnitOfWork.DictionaryRepository.Update(new Dictionary
            {
                ID = model.ID,
                ParentID = model.ParentID,
                Caption = model.Caption,
                DictionaryCode = model.DictionaryCode,
                IntCode = model.IntCode,
                StringCode = model.StringCode,
                IsVisible = model.IsVisible,
                SortVal = model.SortVal

            });

            if (UnitOfWork.DictionaryRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }


            return PartialView("_DictionariesTree", GetDictionariesTreeViewModel());
        }

        [Route("dictionaries/delete", Name = "DictionariesDelete")]
        public ActionResult DeleteDictionary(int? ID)
        {
            var dictionary = UnitOfWork.DictionaryRepository.Get(ID);
            UnitOfWork.DictionaryRepository.Remove(dictionary);

            if (UnitOfWork.DictionaryRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_DictionariesTree", GetDictionariesTreeViewModel());
        }

        DictionariesTreeViewModel GetDictionariesTreeViewModel()
        {
            return new DictionariesTreeViewModel
            {
                ListUrl = Url.RouteUrl("DictionariesList"),
                AddNewUrl = Url.RouteUrl("DictionariesCreate"),
                UpdateUrl = Url.RouteUrl("DictionariesUpdate"),
                DeleteUrl = Url.RouteUrl("DictionariesDelete"),
                TreeItems = UnitOfWork.DictionaryRepository.GetAll().Select(d => new DictionaryTreeItem
                {
                    ID = d.ID,
                    Caption = d.Caption,
                    DictionaryCode = d.DictionaryCode,
                    StringCode = d.StringCode,
                    IntCode = d.IntCode,
                    ParentID = d.ParentID,
                    IsVisible = d.IsVisible.Value,
                    SortVal = d.SortVal
                }).ToList()
            };
        }

    }
}
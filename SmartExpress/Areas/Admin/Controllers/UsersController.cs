using Core;
using Core.Properties;
using DevExpress.Web.Mvc;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using SmartExpress.Reusable.Extentions;
using SmartExpress.Reusable.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        [Route("users", Name = "Users")]
        public ActionResult Index()
        {
            var model = new UsersViewModel
            {
                GridViewModel = GetGridViewModel()
            };

            return View(model);
        }

        [Route("users/list", Name = "UsersList")]
        public ActionResult UsersList()
        {
            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/add", Name = "UsersAdd")]
        public ActionResult UsersAdd([ModelBinder(typeof(DevExpressEditorsBinder))] UserGridItem model)
        {
            var user = new User
            {
                IDNumber = model.IDNumber,
                Password = model.Password.ToMD5(),
                Address = model.Address,
                CompanyName = model.CompanyName,
                ContractNumber = model.ContractNumber,
                CreateTime = DateTime.Now,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                TelephoneNumber = model.TelephoneNumber,
                RoleID = model.RoleID
            };

            UnitOfWork.UserRepository.Add(user);

            if (UnitOfWork.UserRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/update", Name = "UsersUpdate")]
        public ActionResult UsersUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] UserGridItem model)
        {
            UnitOfWork.UserRepository.Update(new User
            {
                ID = model.ID,
                IDNumber = model.IDNumber,
                Password = model.Password.ToMD5(),
                Address = model.Address,
                CompanyName = model.CompanyName,
                ContractNumber = model.ContractNumber,
                CreateTime = DateTime.Now,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                TelephoneNumber = model.TelephoneNumber,
                RoleID = model.RoleID
            });

            if (UnitOfWork.UserRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }

            return PartialView("_UsersGrid", GetGridViewModel());
        }

        [Route("users/delete", Name = "UsersDelete")]
        public ActionResult UsersDelete(int? id)
        {
            var user = UnitOfWork.UserRepository.Get(id);
            UnitOfWork.UserRepository.Remove(user);
            if (UnitOfWork.UserRepository.IsError)
            {
                throw new Exception(Resources.Abort);
            }
            return PartialView("_UsersGrid", GetGridViewModel());
        }

        UsersGridViewModel GetGridViewModel()
        {
            return new UsersGridViewModel
            {
                ListUrl = Url.RouteUrl("UsersList"),
                AddNewUrl = Url.RouteUrl("UsersAdd"),
                UpdateUrl = Url.RouteUrl("UsersUpdate"),
                DeleteUrl = Url.RouteUrl("UsersDelete"),
                GridItems = UnitOfWork.UserRepository.GetAll().OrderByDescending(u => u.CreateTime).Select(u => new UserGridItem
                {
                    ID = u.ID,
                    IDNumber = u.IDNumber,
                    Address = u.Address,
                    CompanyName = u.CompanyName,
                    ContractNumber = u.ContractNumber,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    TelephoneNumber = u.TelephoneNumber,
                    RoleID = u.RoleID,
                }).ToList(),
                Roles = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 1, true).Select(r => new SimpleKeyValueObject<int?, string>
                {
                    Key = r.ID,
                    Value = r.Caption
                }).ToList()
            };
        }

    }
}
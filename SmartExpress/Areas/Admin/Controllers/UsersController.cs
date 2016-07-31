using Core;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartExpress.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class UsersController : BaseController
    {
        [Route("users", Name = "Users")]
        public ActionResult Index()
        {
            var model = new UserViewModel
            {
                UserCreateUrl = Url.RouteUrl("UsersCreate"),
                Users = UnitOfWork.UserRepository.GetAll().Select(u => new UserObject
                {
                    IDNumber = u.IDNumber,
                    Address = u.Address,
                    CompanyName = u.CompanyName,
                    ContractNumber = u.ContractNumber,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    TelephoneNumber = u.TelephoneNumber,
                    RoleID = u.RoleID
                }).ToList()
            };

            return View(model);
        }

        [Route("users/create", Name = "UsersCreate")]
        public ActionResult CreateUser()
        {
            var model = new CreateEditUserViewModel
            {
                SaveUrl = Url.RouteUrl("UsersCreate"),
                UsersUrl = Url.RouteUrl("Users"),
                Title = "მომხმარებლის დამატება"

            };

            return View("CreateEditUser", model);
        }

        [HttpPost]
        [Route("users/create")]
        public ActionResult CreateUser(UserObject model)
        {
            UnitOfWork.UserRepository.Add(new User
            {
                IDNumber = model.IDNumber,
                Address = model.Address,
                CompanyName = model.CompanyName,
                ContractNumber = model.ContractNumber,
                CreateTime = DateTime.Now,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                TelephoneNumber = model.TelephoneNumber,
                RoleID = model.RoleID
            });

            UnitOfWork.Complate();
            UnitOfWork.Dispose();
            return Redirect(Url.RouteUrl("UsersCreate"));
        }

        [Route("users/{ID}/edit", Name = "UsersEdit")]
        public ActionResult EditUser(int? ID)
        {
            var user = UnitOfWork.UserRepository.Get(ID);
            if (user == null)
            {
                return null;
            }
            else
            {
                var userObject = new UserObject
                {
                    IDNumber = user.IDNumber,
                    Address = user.Address,
                    CompanyName = user.CompanyName,
                    ContractNumber = user.ContractNumber,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    TelephoneNumber = user.TelephoneNumber,
                    RoleID = user.RoleID
                };

                return View("CreateEditUser", new CreateEditUserViewModel
                {
                    UserObject = userObject,
                    Title = "მომხმარებლის რედაქტირება",
                    SaveUrl = Url.RouteUrl("UsersEdit", new { ID = user.ID })
                });
            }
        }

        [HttpPost]
        [Route("users/{ID}/edit")]
        public ActionResult EditUser(UserObject model)
        {

            UnitOfWork.UserRepository.Update(new User
            {
                ID = model.ID,
                IDNumber = model.IDNumber,
                Address = model.Address,
                CompanyName = model.CompanyName,
                ContractNumber = model.ContractNumber,
                CreateTime = DateTime.Now,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                TelephoneNumber = model.TelephoneNumber,
                RoleID = model.RoleID
            });

            UnitOfWork.Complate();
            UnitOfWork.Dispose();

            return Redirect(Url.RouteUrl("UsersEdit", new { ID = model.ID }));
        }

        [HttpPost]
        [Route("users/{ID}/delete", Name = "UsersDelete")]
        public ActionResult DeleteUser(int? ID)
        {
            UnitOfWork.UserRepository.Remove(new User { ID = ID });
            UnitOfWork.Complate();
            UnitOfWork.Dispose();

            return Redirect(Url.RouteUrl("Users"));
        }
    }
}
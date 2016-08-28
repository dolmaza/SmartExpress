using Core;
using Core.Properties;
using Core.Utilities;
using Core.Validation;
using SmartExpress.Admin.Models;
using SmartExpress.Admin.Reusable;
using SmartExpress.Reusable.Extentions;
using System;
using System.Data.Entity;
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
                Users = UnitOfWork.UserRepository.GetAll().Include(r => r.Role).ToList().Select(u => new UserObject
                {
                    IDNumber = u.IDNumber,
                    Address = u.Address,
                    CompanyName = u.CompanyName,
                    ContractNumber = u.ContractNumber,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    TelephoneNumber = u.TelephoneNumber,
                    RoleID = u.RoleID,
                    RoleCaption = u.Role.Caption,
                    UserEditUrl = Url.RouteUrl("UsersEdit", new { ID = u.ID }),
                    UserDeleteUrl = Url.RouteUrl("UsersDelete", new { ID = u.ID })
                }).ToList()
            };

            return View(model);
        }

        [Route("users/create", Name = "UsersCreate")]
        public ActionResult CreateUser()
        {
            GenerateSuccessErrorMessageContainer();

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
            var ajaxResponse = new AjaxResponse();
            var errors = Validation.ValidateCreateEditUserFrom(model.ContractNumber);
            if (errors.Count == 0)
            {
                var user = new User
                {
                    IDNumber = model.IDNumber,
                    Password = model.Password.HashPassword(),
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
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Abort
                    };
                }
                else
                {
                    ajaxResponse.IsSuccess = true;
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Success,
                        RedirectUrl = Url.RouteUrl("UsersEdit", new { ID = user.ID })
                    };
                }
            }
            else
            {
                ajaxResponse.Data = new
                {
                    ErrorsJson = errors.ToJson()
                };
            }


            return Json(ajaxResponse);
        }

        [Route("users/{ID}/edit", Name = "UsersEdit")]
        public ActionResult EditUser(int? ID)
        {
            GenerateSuccessErrorMessageContainer();
            var user = UnitOfWork.UserRepository.Get(ID);
            if (user == null)
            {
                return NotFound();
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
                    UsersUrl = Url.RouteUrl("Users"),
                    SaveUrl = Url.RouteUrl("UsersEdit", new { ID = user.ID })
                });
            }
        }

        [HttpPost]
        [Route("users/{ID}/edit")]
        public ActionResult EditUser(UserObject model)
        {
            var ajaxResponse = new AjaxResponse();
            var errors = Validation.ValidateCreateEditUserFrom(model.ContractNumber);
            if (errors.Count == 0)
            {
                UnitOfWork.UserRepository.Update(new User
                {
                    ID = model.ID,
                    IDNumber = model.IDNumber,
                    Password = model.Password.HashPassword(),
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
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Abort
                    };
                }
                else
                {
                    ajaxResponse.IsSuccess = true;
                    ajaxResponse.Data = new
                    {
                        Message = Resources.Success
                    };
                }
            }
            else
            {
                ajaxResponse.Data = new
                {
                    ErrorsJson = errors.ToJson()
                };
            }


            return Json(ajaxResponse);
        }

        [Route("users/{ID}/delete", Name = "UsersDelete")]
        public ActionResult DeleteUser(int? ID)
        {
            var user = UnitOfWork.UserRepository.Get(ID);
            UnitOfWork.UserRepository.Remove(user);
            if (UnitOfWork.UserRepository.IsError)
            {
                InitErrorMessage(Resources.Abort);
            }
            else
            {
                InitSuccessMessage(Resources.Success);
            }
            return Redirect(Url.RouteUrl("Users"));
        }
    }
}
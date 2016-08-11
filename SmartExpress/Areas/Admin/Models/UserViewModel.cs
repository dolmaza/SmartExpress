using Core.Properties;
using SmartExpress.Admin.Reusable;
using SmartExpress.Reusable.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SmartExpress.Admin.Models
{
    public class UserViewModel
    {
        public string UserCreateUrl { get; set; }
        public string ConfirmDeleteText { get; set; } = Resources.TextConfirmDelete;

        public List<UserObject> Users { get; set; }

    }

    public class CreateEditUserViewModel : BaseViewModel
    {
        public string SaveUrl { get; set; }
        public string UsersUrl { get; set; }
        public string Title { get; set; }

        public List<SimpleKeyValueObject<int?, string>> Roles { get; set; }

        public UserObject UserObject { get; set; }

        public CreateEditUserViewModel()
        {
            UserObject = new UserObject();
            Roles = UnitOfWork.DictionaryRepository.GetAllByCodeAndLevel(1, 1, true).Select(d => new SimpleKeyValueObject<int?, string>
            {
                Key = d.ID,
                Value = d.Caption

            }).ToList();
        }

    }

    public class UserObject
    {
        public int? ID { get; set; }
        public string ContractNumber { get; set; }
        public string Password { get; set; }
        public string IDNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string CompanyName { get; set; }

        public int? RoleID { get; set; }
        public string RoleCaption { get; set; }

        public string UserEditUrl { get; set; }
        public string UserDeleteUrl { get; set; }

    }
}
namespace SmartExpress.Admin.Models
{
    public class UserViewModel
    {
        public string UserCreateUrl { get; set; }
        public string ConfirmDeleteText { get; set; }

    }

    public class CreateEditUserViewModel
    {
        public string SaveUrl { get; set; }
        public string UsersUrl { get; set; }
        public string Title { get; set; }

    }
}
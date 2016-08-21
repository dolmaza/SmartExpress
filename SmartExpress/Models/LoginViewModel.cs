namespace SmartExpress.Models
{
    public class LoginViewModel
    {
        public string ContractNumber { get; set; }
        public string Password { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public string SaveUrl { get; set; }

    }
}
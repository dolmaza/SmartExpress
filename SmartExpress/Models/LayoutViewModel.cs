namespace SmartExpress.Models
{
    public class LayoutViewModel
    {
        public string LoginUrl { get; set; }
        public string LogoutUrl { get; set; }
        public string HomeUrl { get; set; }
        public string DashboardUrl { get; set; }
        public bool IsUserAuthorized { get; set; }
        public bool IsAdmin { get; set; }

    }
}
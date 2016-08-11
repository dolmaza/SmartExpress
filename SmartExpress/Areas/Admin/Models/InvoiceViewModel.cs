using Core.Properties;

namespace SmartExpress.Admin.Models
{
    public class InvoiceViewModel
    {
        public string InvoiceCreateUrl { get; set; }
        public string ConfirmDeleteText { get; set; } = Resources.TextConfirmDelete;

    }

    public class CreateEditInvoiceViewModel
    {
        public string SaveUrl { get; set; }
        public string InvoicesUrl { get; set; }
        public string Title { get; set; }

    }
}
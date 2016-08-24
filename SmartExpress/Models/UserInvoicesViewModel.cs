using System.Collections.Generic;

namespace SmartExpress.Models
{
    public class UserInvoicesViewModel
    {
        public List<UserInvoiceObject> UserInvoceObjects { get; set; }

        public class UserInvoiceObject
        {
            public int? ID { get; set; }
            public string InvoiceNumber { get; set; }
            public string ReceiverDate { get; set; }
            public string MessageModeCaption { get; set; }

            public string DetailsUrl { get; set; }

        }

    }

    public class UserInvoiceDetailsViewModel
    {
        public List<UserInvoiceDetailObject> UserInvoiceDetailObjects { get; set; }

        public class UserInvoiceDetailObject
        {
            public int? ID { get; set; }
            public string ReceiverFirstname { get; set; }
            public string ReceiverLastname { get; set; }
            public string ReceiverAddress { get; set; }
            public string DeliveryDate { get; set; }
            public string WhoReceived { get; set; }
        }

    }
}
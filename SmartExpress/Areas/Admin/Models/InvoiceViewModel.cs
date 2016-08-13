using Core.Properties;
using System;

namespace SmartExpress.Admin.Models
{
    public class InvoiceViewModel
    {
        public string InvoiceCreateUrl { get; set; }
        public string ConfirmDeleteText { get; set; } = Resources.TextConfirmDelete;

        public string InvoicesJson { get; set; }


    }

    public class InvoiceObject
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }

        public string InvoiceNumber { get; set; }
        public int? MessageTypeID { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Direction { get; set; }
        public string MessageMode { get; set; }
        public int? MessageModeID { get; set; }
        public int? PayerID { get; set; }
        public int? FormOfPaymentID { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Weigth { get; set; }

        public int? UserID { get; set; }
        public string CompanyName { get; set; }
        public string SenderFirstname { get; set; }
        public string SenderLastname { get; set; }
        public string SenderTelephoneNumber { get; set; }
        public string SenderAddress { get; set; }

        public string ReceiverFirstnameLastname { get; set; }
        public string ReceiverFirstname { get; set; }
        public string ReceiverLastname { get; set; }
        public string ReceiverTelephoneNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public string WhoReceived { get; set; }
        public string WhoReceivedAdditional { get; set; }

    }

    public class CreateEditInvoiceViewModel
    {
        public string SaveUrl { get; set; }
        public string InvoicesUrl { get; set; }
        public string Title { get; set; }

        public InvoiceObject InvoiceObject { get; set; }


    }
}
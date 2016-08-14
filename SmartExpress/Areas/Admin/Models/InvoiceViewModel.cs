using Core.Properties;
using SmartExpress.Reusable.Utilities;
using System.Collections.Generic;

namespace SmartExpress.Admin.Models
{
    public class InvoiceViewModel
    {
        public string InvoiceCreateUrl { get; set; }
        public string InvoicesByReceiveDateUrl { get; set; }
        public string ConfirmDeleteText { get; set; } = Resources.TextConfirmDelete;
        public string CustomDateFormatJs { get; set; } = Resources.CustomDateFormatJs;
        public string CustomDateFormat { get; set; } = Resources.CustomDateFormat;

        public string InvoicesJson { get; set; }


    }

    public class InvoiceObject
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }

        public string InvoiceNumber { get; set; }
        public int? MessageTypeID { get; set; }
        public string ReceiveDate { get; set; }
        public string DeliveryDate { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Direction { get; set; }
        public string MessageMode { get; set; }
        public int? MessageModeID { get; set; }
        public int? PayerID { get; set; }
        public int? FormOfPaymentID { get; set; }
        public string Quantity { get; set; }
        public string Weigth { get; set; }

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

        public List<SimpleKeyValueObject<int?, string>> MessageTypes { get; set; }
        public List<SimpleKeyValueObject<int?, string>> MessageModes { get; set; }
        public List<SimpleKeyValueObject<int?, string>> Payers { get; set; }
        public List<SimpleKeyValueObject<int?, string>> FormOfPaments { get; set; }
        public List<SimpleKeyValueObject<int?, string>> ContractNumbers { get; set; }



    }

    public class CreateEditInvoiceViewModel
    {
        public string SaveUrl { get; set; }
        public string InvoicesUrl { get; set; }
        public string AddNewInvoiceUrl { get; set; }
        public string GetSenderInformationUrl { get; set; }
        public string Title { get; set; }
        public bool HasAddNewButton { get; set; }

        public string CustomDateFormatJs { get; set; } = Resources.CustomDateFormatJs;

        public InvoiceObject InvoiceObject { get; set; }


    }
}
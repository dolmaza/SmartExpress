using System;

namespace Core
{
    public class Invoice
    {
        public int? ID { get; set; }
        public string InvoiceNumber { get; set; }
        public int? MessageTypeID { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Direction { get; set; }
        public int? MessageModeID { get; set; }
        public int? PayerID { get; set; }
        public int? FormOfPaymentID { get; set; }

        public int? UserID { get; set; }
        public string CompanyName { get; set; }
        public string SenderFirstname { get; set; }
        public string SenderLastname { get; set; }
        public string SenderTelephoneNumber { get; set; }
        public string SenderAddress { get; set; }

        public string ReceiverFirstname { get; set; }
        public string ReceiverLastname { get; set; }
        public string ReceiverTelephoneNumber { get; set; }
        public string ReceiverAddress { get; set; }
        public string WhoReceived { get; set; }
        public string WhoReceivedAdditional { get; set; }
        public DateTime? CreateTime { get; set; }

        public User User { get; set; }
        public Dictionary MessageType { get; set; }
        public Dictionary MessageModel { get; set; }
        public Dictionary Payer { get; set; }
        public Dictionary FormOfPayment { get; set; }

    }
}

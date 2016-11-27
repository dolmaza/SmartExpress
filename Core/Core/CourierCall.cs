namespace Core
{
    public class CourierCall
    {
        public int? ID { get; set; }
        public int? CourierCallerTypeID { get; set; }
        public int? StatusID { get; set; }
        public string CourierCallerContractNubmer { get; set; }
        public string CourierCallerPersonalNumber { get; set; }
        public string CourierCallerCompanyName { get; set; }
        public string CourierCallerFirstname { get; set; }
        public string CourierCallerLastname { get; set; }
        public string CourierCallerMobileNumber { get; set; }
        public string CourierCallerEmail { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public int? MessageTypeID { get; set; }
        public string MessageQuantity { get; set; }
        public decimal? TotalWeight { get; set; }
        public int? ServiceTypeID { get; set; }
        public int? PayerTypeID { get; set; }
        public string PayerContractNumber { get; set; }
        public string PayerAddress { get; set; }
        public string PayerCompanyName { get; set; }

        public Dictionary CourierCallerType { get; set; }
        public Dictionary MessageType { get; set; }
        public Dictionary Service { get; set; }
        public Dictionary Payer { get; set; }
        public Dictionary Status { get; set; }


    }
}

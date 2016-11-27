using SmartExpress.Reusable.Utilities;
using System.Collections.Generic;

namespace SmartExpress.Models
{
    public class HomeViewModel
    {
        public CourierCallJuridicalViewModel JuridicalPerson { get; set; }
        public CourierCallPhysicalViewModel PhysicalPerson { get; set; }

    }

    public class CourierCallViewModelBase
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public int? MessageTypeID { get; set; }
        public decimal? MessageQuantity { get; set; }
        public decimal? TotalWeight { get; set; }
        public int? ServiceTypeID { get; set; }

        public List<SimpleKeyValueObject<int?, string>> ServiceTypes { get; set; }
        public List<SimpleKeyValueObject<int?, string>> MessageTypes { get; set; }

    }

    public class CourierCallJuridicalViewModel : CourierCallViewModelBase
    {
        public string ContractNumber { get; set; }
        public string CompanyName { get; set; }

        public string PayerID { get; set; }
        public string PayerContractNumber { get; set; }
        public string PayerAddress { get; set; }
        public string PayerCompanyName { get; set; }

        public List<SimpleKeyValueObject<int?, string>> PayerTypes { get; set; }

    }

    public class CourierCallPhysicalViewModel : CourierCallViewModelBase
    {
        public string PersonalID { get; set; }

    }
}
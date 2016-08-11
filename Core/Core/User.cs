using System;
using System.Collections.Generic;

namespace Core
{
    public class User
    {
        public int? ID { get; set; }
        public string ContractNumber { get; set; }
        public string IDNumber { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string CompanyName { get; set; }
        public int? RoleID { get; set; }
        public DateTime? CreateTime { get; set; }

        public Dictionary Role { get; set; }
        public IList<Invoice> Invoices { get; set; }

        public User()
        {
            Invoices = new List<Invoice>();
        }

    }
}

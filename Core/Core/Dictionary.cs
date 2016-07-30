using System;
using System.Collections.Generic;

namespace Core
{
    public class Dictionary
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string StringCode { get; set; }
        public int? IntCode { get; set; }
        public int? Level { get; set; }
        public string Hierarchy { get; set; }
        public bool IsVisible { get; set; }
        public int? SortVal { get; set; }
        public DateTime? CreateTime { get; set; }

        public IList<User> Roles { get; set; }

        public IList<Invoice> MessageTypes { get; set; }
        public IList<Invoice> MessageModes { get; set; }
        public IList<Invoice> Payers { get; set; }
        public IList<Invoice> FormOfPayments { get; set; }

        public Dictionary()
        {
            MessageTypes = new List<Invoice>();
            MessageModes = new List<Invoice>();
            Payers = new List<Invoice>();
            FormOfPayments = new List<Invoice>();
            Roles = new List<User>();
        }
    }
}

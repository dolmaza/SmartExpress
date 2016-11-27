using SmartExpress.Admin.Reusable;
using System.Collections.Generic;

namespace SmartExpress.Admin.Models
{
    public class DictionariesViewModel : BaseViewModel
    {
        public DictionariesTreeViewModel TreeViewModel { get; set; }

    }

    public class DictionariesTreeViewModel : TreeListVeiwModelBase
    {
        public List<DictionaryTreeItem> TreeItems { get; set; }

    }

    public class DictionaryTreeItem
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string StringCode { get; set; }
        public int? IntCode { get; set; }
        public int? DictionaryCode { get; set; }
        public bool? IsVisible { get; set; }
        public int? SortVal { get; set; }
    }
}
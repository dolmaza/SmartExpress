using Core.Properties;
using SmartExpress.Admin.Reusable;

namespace SmartExpress.Admin.Models
{
    public class DictionaryViewModel
    {
        public string DictionaryCreateUrl { get; set; }
        public string ConfirmDeleteText { get; set; } = Resources.TextConfirmDelete;
        public string DictionariesJson { get; set; }

    }

    public class DictionaryObject
    {
        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Caption { get; set; }
        public string StringCode { get; set; }
        public int? IntCode { get; set; }
        public int? DictionaryCode { get; set; }
        public bool IsVisible { get; set; }
        public int? SortVal { get; set; }
    }

    public class CreateEditDictionaryViewModel : BaseViewModel
    {
        public string SaveUrl { get; set; }
        public string Title { get; set; }

        public DictionaryObject DictionaryObject { get; set; }

    }
}
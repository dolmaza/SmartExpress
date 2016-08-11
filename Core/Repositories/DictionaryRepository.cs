using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IDictionaryRepository : IRepositoy<Dictionary>
    {
        List<Dictionary> GetAllByCodeAndLevel(int? level = null, int? dictionaryCode = null, bool? isVisible = null);
    }

    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(DbContext context)
            : base(context)
        {
        }

        public List<Dictionary> GetAllByCodeAndLevel(int? level, int? dictionaryCode, bool? isVisible)
        {
            return Find(d => d.Level == level && d.DictionaryCode == dictionaryCode && d.IsVisible == isVisible).ToList();
        }
    }
}

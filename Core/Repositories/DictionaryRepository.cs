using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IDictionaryRepository : IRepositoy<Dictionary>
    {
        List<Dictionary> GetAll(int? level = null, int? dictionaryCode = null, bool? isVisible = null);
    }

    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(DbContext context)
            : base(context)
        {
        }

        public List<Dictionary> GetAll(int? level = null, int? dictionaryCode = null, bool? isVisible = null)
        {
            return Find(d => d.Level == level && d.DictionaryCode == dictionaryCode && d.IsVisible == isVisible).ToList();
        }
    }
}

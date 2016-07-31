using System.Data.Entity;

namespace Core.Repositories
{
    public interface IInvoiceRepository : IRepositoy<Invoice>
    {

    }

    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(DbContext context)
            : base(context)
        {
        }
    }
}

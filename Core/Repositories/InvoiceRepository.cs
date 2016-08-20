using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IInvoiceRepository : IRepositoy<Invoice>
    {
        IEnumerable<Invoice> GetInvoicesForExportExcel(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<Invoice> GetInvociesByReceiveDate(DateTime? dateFrom, DateTime? dateTo);
    }

    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Invoice> GetInvociesByReceiveDate(DateTime? dateFrom, DateTime? dateTo)
        {
            return Find(i => (dateFrom == null && dateTo == null)
                        || (dateFrom != null && dateTo == null && i.ReceiveDate >= dateFrom)
                        || (dateFrom == null && dateTo != null && i.ReceiveDate <= dateTo)
                        || (dateFrom != null && dateTo != null && i.ReceiveDate >= dateFrom && i.ReceiveDate <= dateTo))
                .OrderByDescending(i => i.ReceiveDate)
                .Include(i => i.MessageMode)
                .ToList();
        }

        public IEnumerable<Invoice> GetInvoicesForExportExcel(DateTime? dateFrom, DateTime? dateTo)
        {
            return Find(i => (dateFrom == null && dateTo == null)
                        || (dateFrom != null && dateTo == null && i.ReceiveDate >= dateFrom)
                        || (dateFrom == null && dateTo != null && i.ReceiveDate <= dateTo)
                        || (dateFrom != null && dateTo != null && i.ReceiveDate >= dateFrom && i.ReceiveDate <= dateTo))
                        .Include(i => i.MessageType)
                        .Include(i => i.MessageMode)
                        .Include(i => i.Payer)
                        .Include(i => i.FormOfPayment)
                        .Include(i => i.User).ToList();
        }
    }
}

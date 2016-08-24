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
        IEnumerable<Invoice> GetUserInvoices(int? userID);
        IEnumerable<Invoice> GetUserInvoiceDetails(int? userID, string invoiceNumber);
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

        public IEnumerable<Invoice> GetUserInvoices(int? userID)
        {
            return GetAll()
                .Where(i => i.User.ID == userID && i.ParentID == null)
                .Include(i => i.User)
                .Include(i => i.MessageMode)
                .ToList()
                .Select(i => new Invoice
                {
                    ID = i.ID,
                    InvoiceNumber = i.InvoiceNumber,
                    ReceiveDate = i.ReceiveDate,
                    MessageModeID = i.MessageModeID,
                    MessageMode = i.MessageMode

                })
                .ToList();
        }

        public IEnumerable<Invoice> GetUserInvoiceDetails(int? userID, string invoiceNumber)
        {
            return GetAll()
                .Where(i => i.User.ID == userID && i.InvoiceNumber == invoiceNumber)
                .ToList()
                .Select(i => new Invoice
                {
                    ID = i.ID,
                    ReceiverFirstname = i.ReceiverFirstname,
                    ReceiverLastname = i.ReceiverLastname,
                    ReceiverAddress = i.ReceiverAddress,
                    DeliveryDate = i.DeliveryDate,
                    WhoReceived = i.WhoReceived

                })
                .ToList();
        }
    }
}

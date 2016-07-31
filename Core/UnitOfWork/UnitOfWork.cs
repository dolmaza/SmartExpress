using Core.DB;
using Core.Repositories;
using System;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDictionaryRepository DictionaryRepository { get; }
        IUserRepository UserRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        int Complate();

    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbCoreDataContext _context;

        public IDictionaryRepository DictionaryRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IInvoiceRepository InvoiceRepository { get; private set; }

        public UnitOfWork(DbCoreDataContext context)
        {
            _context = context;
            DictionaryRepository = new DictionaryRepository(_context);
            UserRepository = new UserRepository(_context);
            InvoiceRepository = new InvoiceRepository(_context);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complate()
        {
            return _context.SaveChanges();
        }
    }
}

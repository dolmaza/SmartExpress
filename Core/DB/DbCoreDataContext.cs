using Core.DB.Configuration;
using System.Data.Entity;

namespace Core.DB
{
    public class DbCoreDataContext : DbContext
    {
        public DbCoreDataContext()
            : base("name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DictionaryConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

    }
}

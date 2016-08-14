using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configuration
{
    public class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            ToTable("Invoices");

            HasKey(i => i.ID);

            Property(i => i.CompanyName)
                .HasMaxLength(500);

            Property(i => i.Direction)
                .HasMaxLength(500);

            Property(i => i.Quantity)
                .HasColumnType("money");

            Property(i => i.Weigth)
                .HasColumnType("money");

            Property(i => i.SenderAddress)
                .HasMaxLength(500);

            Property(i => i.SenderFirstname)
                .HasMaxLength(500);

            Property(i => i.SenderLastname)
                .HasMaxLength(500);


            Property(i => i.ReceiverTelephoneNumber)
                .HasMaxLength(20);

            Property(i => i.ReceiverAddress)
                .HasMaxLength(500);

            Property(i => i.ReceiverFirstname)
                .HasMaxLength(500);

            Property(i => i.ReceiverLastname)
                .HasMaxLength(500);


            Property(i => i.SenderTelephoneNumber)
                .HasMaxLength(30);


            Property(i => i.WhoReceived)
                .HasMaxLength(500);


            Property(i => i.InvoiceNumber)
                .HasMaxLength(30);

            HasOptional(i => i.Parent)
                .WithMany(i => i.Parents)
                .HasForeignKey(i => i.ParentID)
                .WillCascadeOnDelete(false);


            HasOptional(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserID)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.FormOfPayment)
                .WithMany(d => d.FormOfPayments)
                .HasForeignKey(i => i.FormOfPaymentID)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.MessageMode)
                .WithMany(d => d.MessageModes)
                .HasForeignKey(i => i.MessageModeID)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.MessageType)
                .WithMany(d => d.MessageTypes)
                .HasForeignKey(i => i.MessageTypeID)
                .WillCascadeOnDelete(false);

            HasOptional(i => i.Payer)
                .WithMany(d => d.Payers)
                .HasForeignKey(i => i.PayerID)
                .WillCascadeOnDelete(false);

        }
    }
}

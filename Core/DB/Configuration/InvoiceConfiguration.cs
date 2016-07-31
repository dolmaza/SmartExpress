using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
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

            Property(i => i.SenderAddress)
                .HasMaxLength(500);

            Property(i => i.SenderFirstname)
                .HasMaxLength(500);

            Property(i => i.SenderLastname)
                .HasMaxLength(500);


            Property(i => i.ReceiverTelephoneNumber)
                .HasMaxLength(10);

            Property(i => i.ReceiverAddress)
                .HasMaxLength(500);

            Property(i => i.ReceiverFirstname)
                .HasMaxLength(500);

            Property(i => i.ReceiverLastname)
                .HasMaxLength(500);


            Property(i => i.ReceiverTelephoneNumber)
                .HasMaxLength(10);


            Property(i => i.WhoReceived)
                .HasMaxLength(500);


            Property(i => i.InvoiceNumber)
                .HasMaxLength(30)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_InvoiceNumberUniq")
                        {
                            IsUnique = true
                        }));

            HasRequired(i => i.User)
                .WithMany(u => u.Invoices)
                .HasForeignKey(i => i.UserID)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.FormOfPayment)
                .WithMany(d => d.FormOfPayments)
                .HasForeignKey(i => i.FormOfPaymentID)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.MessageModel)
                .WithMany(d => d.MessageModes)
                .HasForeignKey(i => i.MessageModeID)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.MessageType)
                .WithMany(d => d.MessageTypes)
                .HasForeignKey(i => i.MessageTypeID)
                .WillCascadeOnDelete(false);

            HasRequired(i => i.Payer)
                .WithMany(d => d.Payers)
                .HasForeignKey(i => i.PayerID)
                .WillCascadeOnDelete(false);

        }
    }
}

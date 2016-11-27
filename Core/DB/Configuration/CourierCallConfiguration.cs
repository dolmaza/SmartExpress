using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configuration
{
    public class CourierCallConfiguration : EntityTypeConfiguration<CourierCall>
    {
        public CourierCallConfiguration()
        {
            ToTable("CourierCalls");

            HasKey(c => c.ID);

            Property(c => c.TotalWeight)
                .HasColumnType("money");

            HasRequired(c => c.CourierCallerType)
                .WithMany(c => c.CourierCallTypes)
                .HasForeignKey(c => c.CourierCallerTypeID)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.MessageType)
                .WithMany(c => c.CourierCallMessageTypes)
                .HasForeignKey(c => c.MessageTypeID)
                .WillCascadeOnDelete(false);


            HasRequired(c => c.Payer)
                .WithMany(c => c.CourierCallPayers)
                .HasForeignKey(c => c.PayerTypeID)
                .WillCascadeOnDelete(false);


            HasRequired(c => c.Service)
                .WithMany(c => c.CourierCallServices)
                .HasForeignKey(c => c.ServiceTypeID)
                .WillCascadeOnDelete(false);

            HasRequired(c => c.Status)
                .WithMany(c => c.CourierCallStatuses)
                .HasForeignKey(c => c.StatusID)
                .WillCascadeOnDelete(false);


        }
    }
}

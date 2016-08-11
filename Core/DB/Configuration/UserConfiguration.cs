using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(u => u.ID);

            Property(u => u.Address)
                .HasMaxLength(500);

            Property(u => u.CompanyName)
                .HasMaxLength(500);

            Property(u => u.Password)
                .HasMaxLength(500)
                .IsRequired();

            Property(u => u.ContractNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                        new IndexAttribute("IX_ContractNumberUniq")
                        {
                            IsUnique = true
                        }));

            Property(u => u.Firstname)
                .HasMaxLength(500);

            Property(u => u.Lastname)
                .HasMaxLength(500);

            Property(u => u.TelephoneNumber)
                .HasMaxLength(20);

            Property(u => u.IDNumber)
                .HasMaxLength(20);

            HasRequired(u => u.Role).WithMany(r => r.Roles)
                .HasForeignKey(u => u.RoleID)
                .WillCascadeOnDelete(false);
        }
    }
}

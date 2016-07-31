using System.Data.Entity.ModelConfiguration;

namespace Core.DB.Configuration
{
    public class DictionaryConfiguration : EntityTypeConfiguration<Dictionary>
    {
        public DictionaryConfiguration()
        {
            ToTable("Dictionaries");

            HasKey(d => d.ID);

            Property(d => d.Caption)
                .IsRequired()
                .HasMaxLength(200);

            Property(d => d.DictionaryCode)
                .IsRequired();

            Property(d => d.StringCode)
                .HasMaxLength(500);

            HasOptional(d => d.Parent).WithMany(d => d.Parents)
                .HasForeignKey(d => d.ParentID)
                .WillCascadeOnDelete(false);
        }
    }
}

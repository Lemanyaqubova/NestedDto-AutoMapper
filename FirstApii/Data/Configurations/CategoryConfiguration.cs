using FirstApii.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApii.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(c => c.Desc).HasMaxLength(100).IsRequired(false);
            builder.Property(c => c.CreateDate).HasDefaultValueSql("GetUtcDate()");
            builder.Property(c=>c.UpdateDate ).HasDefaultValueSql("GetUtcDate()");
        }
    }
}

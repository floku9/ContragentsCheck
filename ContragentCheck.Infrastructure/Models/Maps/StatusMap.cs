using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContragentsCheck.Infrastructure.Models.Maps
{
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            {
                builder.HasKey(st => st.Id);
                builder.Property(st => st.Id).ValueGeneratedOnAdd();
                builder.Property(st => st.Name).IsRequired();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ContragentsCheck.Infrastructure.Models.Maps
{
    public class ReportMap : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(rep => rep.Id);
            builder.Property(rep => rep.Id).ValueGeneratedOnAdd();
            builder.Property(rep => rep.ReportLink).IsRequired();
        }
    }
}

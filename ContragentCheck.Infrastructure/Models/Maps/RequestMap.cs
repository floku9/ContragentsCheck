using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContragentsCheck.Infrastructure.Models.Maps
{
    public class RequestMap : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(req => req.Id);
            builder.Property(req => req.Id).ValueGeneratedOnAdd();
            builder.Property(req => req.Inn).HasMaxLength(12).IsRequired();
            builder
                .HasOne(req => req.Report)
                .WithOne(rep => rep.Request)
                .HasForeignKey<Request>(req=>req.ReportId)
                .IsRequired(false);
            builder
                .HasOne(req => req.Status)
                .WithMany(st => st.Requests)
                .HasForeignKey(req => req.StatusId)
                .IsRequired();
            builder.Property(req => req.StatusId).IsConcurrencyToken();
        }
    }
}

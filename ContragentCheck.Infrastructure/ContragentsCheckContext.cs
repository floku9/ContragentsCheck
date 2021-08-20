using ContragentsCheck.Infrastructure.Models;
using ContragentsCheck.Infrastructure.Models.Maps;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContragentsCheck.Infrastructure
{
    public class ContragentsCheckContext : DbContext
    {
        public ContragentsCheckContext() 
        {

        }
        public ContragentsCheckContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
            modelBuilder.ApplyConfiguration(new RequestMap());
            modelBuilder.ApplyConfiguration(new ReportMap());
            modelBuilder.ApplyConfiguration(new StatusMap());

        }
    }
}

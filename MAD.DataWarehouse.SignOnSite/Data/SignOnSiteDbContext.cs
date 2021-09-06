using MAD.DataWarehouse.SignOnSite.Api;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Data
{
    public class SignOnSiteDbContext : DbContext
    {
        public SignOnSiteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Site>(cfg =>
            {
                cfg.HasKey(y => y.Id);
                cfg.Property(y => y.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<SiteAttendance>(cfg =>
            {
                cfg.HasKey(y => y.Id);
                cfg.Property(y => y.Id).ValueGeneratedNever();

                cfg.OwnsOne(y => y.Company);
                cfg.OwnsOne(y => y.User);

                cfg.HasOne(y => y.Site).WithMany();
            });
        }
    }
}

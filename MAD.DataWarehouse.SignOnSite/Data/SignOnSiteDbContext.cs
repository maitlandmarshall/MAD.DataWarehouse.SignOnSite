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

                cfg.HasOne(y => y.Site).WithMany().HasForeignKey(y => y.SiteId);
            });

            modelBuilder.Entity<SiteBriefing>(cfg =>
            {
                cfg.HasKey(y => y.Id);
                cfg.Property(y => y.Id).ValueGeneratedNever();

                cfg.HasOne(y => y.Site).WithMany().HasForeignKey(y => y.SiteId);
            });

            modelBuilder.Entity<BriefingLog>(cfg =>
            {
                cfg.HasKey(y => new { y.BriefingId, y.FirstName, y.LastName, y.Day, y.EarliestAcknowledgedAt });

                cfg.OwnsOne(y => y.Company);
            });

            modelBuilder.Entity<SiteUser>(cfg =>
            {
                cfg.HasKey(y => new { y.Id, y.SiteId });

                cfg.OwnsOne(y => y.Induction, own =>
                {
                    own.OwnsOne(y => y.State, own =>
                    {
                        own.Property(y => y.SetAt).HasColumnType("date");
                        own.OwnsOne(y => y.SetBy);
                    });
                });

                cfg.OwnsOne(y => y.SiteCompany);
            });
        }
    }
}

using MAD.DataWarehouse.SignOnSite.Api;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

            modelBuilder.Entity<SiteAttendee>(cfg =>
            {
                cfg.Property<int>("SiteId").ValueGeneratedNever();

                cfg.HasKey("Id", "SiteId");
                cfg.Property(y => y.Id).ValueGeneratedNever();

                cfg.OwnsOne(y => y.SiteInduction);
                cfg.OwnsMany(y => y.Attendances, cfg =>
                {
                    cfg.WithOwner().HasForeignKey("SiteAttendeeId", "SiteId");
                    cfg.Property(y => y.Id).ValueGeneratedNever();
                    cfg.OwnsOne(y => y.Company);
                });

                cfg.Property(y => y.WorkerNotes).HasConversion(
                    y => y.ToString(),
                    y => JArray.Parse(y));
            });

            modelBuilder.Entity<SiteInduction>(cfg =>
            {
                cfg.HasKey(y => new { y.Id, y.UserId });
                cfg.Property<int>("SiteId").ValueGeneratedNever();

                cfg.Property(y => y.Id).ValueGeneratedNever();
                cfg.Property(y => y.UserId).ValueGeneratedNever();

                cfg.OwnsOne(y => y.SiteCompany);

                cfg.OwnsMany(y => y.UnsubmittedForms, cfg =>
                {
                    cfg.WithOwner().HasForeignKey("Id");
                    cfg.Property(y => y.Id).ValueGeneratedNever();
                });

                cfg.OwnsMany(y => y.CompletedInductions, cfg =>
                {
                    cfg.WithOwner().HasForeignKey("Id");
                    cfg.Property(y => y.Id).ValueGeneratedNever();
                    cfg.OwnsOne(y => y.StatusSetBy);
                });
            });


        }
    }
}

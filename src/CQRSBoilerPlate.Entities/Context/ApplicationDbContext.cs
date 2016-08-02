using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict;
using CQRSBoilerPlate.Entities.DBModels;

namespace CQRSBoilerPlate.Entities.Context
{
    public class ApplicationDbContext : OpenIddictDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<PlanFeature>()
                .HasKey(t => new { t.PlanID, t.FeatureID });

            builder.Entity<PlanFeature>()
                .HasOne(pt => pt.Plan)
                .WithMany(p => p.PlanFeatures)
                .HasForeignKey(pt => pt.PlanID);

            builder.Entity<PlanFeature>()
                .HasOne(pt => pt.Feature)
                .WithMany(t => t.PlanFeatures)
                .HasForeignKey(pt => pt.FeatureID);
        }

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}

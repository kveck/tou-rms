using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data.Models;
using MigrateTOUData.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.Database
{
    internal class VssDbContext : DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationAddress> OrganizationAddresses { get; set; }
        public DbSet<ResourceProgram> ResourcePrograms { get; set; }
        public DbSet<ResourceContact> ResourceContacts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=.;Database=touResourceDatabase;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // organization tables
            builder.ApplyConfiguration(new OrganizationConfiguration());
            builder.ApplyConfiguration(new OrganizationAddressConfiguration());
            
            // resource tables
            builder.ApplyConfiguration(new ResourceProgramConfiguration());
            builder.ApplyConfiguration(new ResourceWithLanguageConfiguration());
            builder.ApplyConfiguration(new ResourceWithContactConfiguration());
            builder.ApplyConfiguration(new ResourceWithApplicationTypeConfiguration());
            builder.ApplyConfiguration(new ResourceWithServiceConfiguration());
            builder.ApplyConfiguration(new ResourceWithRegionConfiguration());
            builder.ApplyConfiguration(new ResourceWithSituationConfiguration());            

            // resource contact tables
            builder.ApplyConfiguration(new ResourceContactConfiguration());
            builder.ApplyConfiguration(new ContactWithLanguageConfiguration());
            builder.ApplyConfiguration(new ContactWithRegionConfiguration());

            // language tables
            builder.ApplyConfiguration(new ResourceLanguageConfiguration());

            // application type
            builder.ApplyConfiguration(new ResourceApplicationTypeConfiguration());

            // region taxonomy tables
            builder.ApplyConfiguration(new RegionTaxonomyConfiguration());

            // human situation taxonomy
            builder.ApplyConfiguration(new SituationTaxonomyConfiguration());

            // human service taxonomy
            builder.ApplyConfiguration(new ServiceTaxonomyConfiguration());

            // user tables
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserDetailConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserWithResourceFavoriteConfiguration());
            

            base.OnModelCreating(builder);
        }
    }
}

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
    internal class RmsDbContext : DbContext
    {

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<OrganizationAddress> OrganizationAddresses { get; set; }

        public virtual DbSet<RegionTaxonomy> RegionTaxonomies { get; set; }

        public virtual DbSet<ContactWithRegion> ContactsWithRegions { get; set; }

        public virtual DbSet<ContactWithLanguage> ContactsWithLanguages { get; set; }

        public virtual DbSet<ResourceWithRegion> ResourcesWithRegions { get; set; }

        public virtual DbSet<ResourceActivity> ResourceActivities { get; set; }

        public virtual DbSet<ResourceActivityDetail> ResourceActivityDetails { get; set; }

        public virtual DbSet<ResourceActivityType> ResourceActivityTypes { get; set; }

        public virtual DbSet<ResourceApplicationType> ResourceApplicationTypes { get; set; }

        public virtual DbSet<ResourceWithApplicationType> ResourcesWithApplicationTypes { get; set; }

        public virtual DbSet<ResourceCodeLegacy> ResourceCodeLegacies { get; set; }

        public virtual DbSet<ResourceContact> ResourceContacts { get; set; }

        public virtual DbSet<ResourceWithContact> ResourcesWithContacts { get; set; }

        public virtual DbSet<ResourceLanguage> ResourceLanguages { get; set; }

        public virtual DbSet<ResourceWithLanguage> ResourcesWithLanguages { get; set; }

        public virtual DbSet<ResourceProcessTime> ResourceProcessTimes { get; set; }

        public virtual DbSet<ResourceProgram> ResourcePrograms { get; set; }

        public virtual DbSet<ResourceProgramDescription> ResourceProgramDescriptions { get; set; }

        public virtual DbSet<ResourceProgramDetail> ResourceProgramDetails { get; set; }

        public virtual DbSet<ResourceProgramNote> ResourceProgramNotes { get; set; }

        public virtual DbSet<ResourceProgramStatus> ResourceProgramStatuses { get; set; }

        public virtual DbSet<ResourceProgramStep> ResourceProgramSteps { get; set; }

        public virtual DbSet<ResourceStatusType> ResourceStatusTypes { get; set; }

        public virtual DbSet<ServiceTaxonomy> ServiceTaxonomies { get; set; }

        public virtual DbSet<ResourceWithService> ResourcesWithServices { get; set; }

        public virtual DbSet<SituationTaxonomy> SituationTaxonomies { get; set; }

        public virtual DbSet<ResourceWithSituation> ResourcesWithSituations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=.;Database=touResourceDatabase;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // organization tables
            builder.ApplyConfiguration(new OrganizationConfiguration());
            builder.ApplyConfiguration(new OrganizationAddressConfiguration());
            
            // resource tables
            builder.ApplyConfiguration(new ResourceProgramConfiguration());
            builder.ApplyConfiguration(new ResourceProgramDetailsConfiguration());
            builder.ApplyConfiguration(new ResourceProcessTimeConfiguration());
            builder.ApplyConfiguration(new ResourceProgramDescriptionConfiguration());
            builder.ApplyConfiguration(new ResourceProgramNotesConfiguration());
            builder.ApplyConfiguration(new ResourceProgramStepConfiguration());
            builder.ApplyConfiguration(new ResourceWithLanguageConfiguration());
            builder.ApplyConfiguration(new ResourceWithContactConfiguration());
            builder.ApplyConfiguration(new ResourceWithApplicationTypeConfiguration());
            builder.ApplyConfiguration(new ResourceWithServiceConfiguration());
            builder.ApplyConfiguration(new ResourceWithRegionConfiguration());
            builder.ApplyConfiguration(new ResourceWithSituationConfiguration());
            builder.ApplyConfiguration(new ResourceStatusConfiguration());
            builder.ApplyConfiguration(new ResourceStatusTypeConfiguration());
            builder.ApplyConfiguration(new ResourceActivityConfiguration());
            builder.ApplyConfiguration(new ResourceActivityDetailConfiguration());
            builder.ApplyConfiguration(new ResourceActivityTypeConfiguration());

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
            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new UserDetailConfiguration());
            //builder.ApplyConfiguration(new UserRoleConfiguration());
            //builder.ApplyConfiguration(new UserWithResourceFavoriteConfiguration());
            

            base.OnModelCreating(builder);
        }
    }
}

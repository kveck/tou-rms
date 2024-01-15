﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MigrateTOUData.Data.Models;

public partial class ResourceProgram
{
    public int Id { get; set; }

    public int ResourceCode { get; set; }

    public int OrgId { get; set; }

    public string Name { get; set; }

    public string ResourceUrl { get; set; }

    public string Status { get; set; }

    public DateTime TimestampLastUpdate { get; set; }

    public DateTime TimestampCreated { get; set; }

    public virtual Organization Org { get; set; }

    public virtual ICollection<ResourceProgramDetail> ProgramDetails { get; set; } = new List<ResourceProgramDetail>();

    public virtual ICollection<ResourceProgramDescription> Descriptions { get; set; } = new List<ResourceProgramDescription>();
    
    public virtual ICollection<ResourceProgramNote> ResourceProgramNotes { get; set; } = new List<ResourceProgramNote>();

    public virtual ICollection<ResourceProgramStep> ResourceProgramSteps { get; set; } = new List<ResourceProgramStep>();

public virtual ICollection<ResourceWithRegion> ResourceWithRegions { get; set; } = new List<ResourceWithRegion>();

    public virtual ICollection<ResourceWithApplicationType> ResourceWithApplicationTypes { get; set; } = new List<ResourceWithApplicationType>();

    public virtual ICollection<ResourceWithContact> ResourceWithContacts { get; set; } = new List<ResourceWithContact>();

    public virtual ICollection<ResourceWithLanguage> ResourceWithLanguages { get; set; } = new List<ResourceWithLanguage>();

    public virtual ICollection<ResourceWithService> ResourceWithServices { get; set; } = new List<ResourceWithService>();

    public virtual ICollection<ResourceWithSituation> ResourceWithSituations { get; set; } = new List<ResourceWithSituation>();

    public virtual ICollection<UserWithResourceFavorite> ResourceWithUserFavorites { get; set; } = new List<UserWithResourceFavorite>();

    public virtual ICollection<UserWithResourceRating> ResourceWithUserRatings { get; set; } = new List<UserWithResourceRating>();
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MigrateTOUData.Data.Models;

public partial class ResourceProgram
{
    public int Id { get; set; }

    public int ResourceCode { get; set; }

    public int OrgId { get; set; }

    public int DetailId { get; set; }

    public string Name { get; set; }

    public string ResourceUrl { get; set; }

    public int StatusId { get; set; }

    public DateTime TimestampCreated { get; set; }

    public virtual ResourceProgramDetail Detail { get; set; }

    public virtual ResourceProgramDetail ResourceProgramDetail { get; set; }

    public virtual ResourceProgramStatus Status { get; set; }

    public virtual Organization Org { get; set; }

    public virtual ICollection<ResourceActivity> ResourceActivities { get; set; } = new List<ResourceActivity>();

    // stores status history for the resource
    [JsonIgnore]
    public virtual ICollection<ResourceProgramStatus> ResourceStatuses { get; set; } = new List<ResourceProgramStatus>();

    public virtual ICollection<ResourceCodeLegacy> ResourceCodeLegacies { get; set; } = new List<ResourceCodeLegacy>();

    public virtual ICollection<ResourceWithRegion> ResourceWithRegions { get; set; } = new List<ResourceWithRegion>();

    public virtual ICollection<ResourceWithApplicationType> ResourceWithApplicationTypes { get; set; } = new List<ResourceWithApplicationType>();

    public virtual ICollection<ResourceWithContact> ResourceWithContacts { get; set; } = new List<ResourceWithContact>();

    public virtual ICollection<ResourceWithLanguage> ResourceWithLanguages { get; set; } = new List<ResourceWithLanguage>();

    public virtual ICollection<ResourceWithService> ResourceWithServices { get; set; } = new List<ResourceWithService>();

    public virtual ICollection<ResourceWithSituation> ResourceWithSituations { get; set; } = new List<ResourceWithSituation>();

    public virtual ICollection<UserWithResourceFavorite> ResourceWithUserFavorites { get; set; } = new List<UserWithResourceFavorite>();

    public virtual ICollection<UserWithResourceRating> ResourceWithUserRatings { get; set; } = new List<UserWithResourceRating>();
}
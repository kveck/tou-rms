﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MigrateTOUData.Data.Models;

public partial class ResourceStatusType
{
    public int Id { get; set; }

    public string StatusType { get; set; }

    [JsonIgnore]
    public virtual ICollection<ResourceProgramStatus> ResourceStatuses { get; set; } = new List<ResourceProgramStatus>();
}
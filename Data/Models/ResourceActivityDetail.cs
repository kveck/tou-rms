﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MigrateTOUData.Data.Models;

public partial class ResourceActivityDetail
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public string ActivityDetail { get; set; }

    public DateTime Timestamp { get; set; }

    [JsonIgnore]
    public virtual ResourceActivity Activity { get; set; }
}
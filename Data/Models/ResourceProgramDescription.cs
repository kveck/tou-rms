﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MigrateTOUData.Data.Models;

public partial class ResourceProgramDescription
{
    public int Id { get; set; }

    public int ResourceDetailId { get; set; }

    public string Description { get; set; }

    public virtual ResourceProgramDetail ResourceDetail { get; set; }
}
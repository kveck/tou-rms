﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MigrateTOUData.Data.Models;

public partial class ResourceCodeLegacy
{
    public int Id { get; set; }

    public int ResourceId { get; set; }

    public int LegacyCode { get; set; }

    public virtual ResourceProgram Resource { get; set; }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MigrateTOUData.Data.Models;

public partial class OrganizationAddress
{
    public int Id { get; set; }

    public int OrgId { get; set; }

    public string Street1 { get; set; }

    public string Street2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zip { get; set; }

    public string ZipExt { get; set; }

    public string Country { get; set; }

    public virtual Organization Org { get; set; }
}
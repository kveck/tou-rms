﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MigrateTOUData.Data.Models;

public partial class ContactWithLanguage
{
    public int Id { get; set; }

    public int LanguageId { get; set; }

    public int ContactId { get; set; }

    public virtual ResourceContact Contact { get; set; }

    public virtual ResourceLanguage Language { get; set; }
}
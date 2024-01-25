using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MigrateTOUData.Data;
using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using System;
using System.Resources;
using System.Runtime.InteropServices;

namespace MigrateTOUData
{

//1. Register your DbContext class in your "Program.cs" file.

//    ```csharp
//    builder.Services.AddDbContext<touResourceDatabaseContext>(
//        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//    ```

//2. Add "ConnectionStrings" to your configuration file(secrets.json, appsettings.Development.json or appsettings.json).

//    ```json
//    {
//        "ConnectionStrings": {
//            "DefaultConnection": "Data Source=KRISXPS;Initial Catalog=touResourceDatabase;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
//        }
//    }
//    ```

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            try
            {
                using (var dbContext = new RmsDbContext())
                {
                    var newOrg = dbContext.Add(new Organization
                    {
                        Name = "HomeStart",
                        OrgCode = "HOMES",
                        Url = @"https://www.homestart.org",
                        Email = "",
                        Phone = "6175420338",
                        Fax = "6175421454",
                        NextResourceId = 2,
                        OrganizationAddresses = new List<OrganizationAddress>
                        {
                            new OrganizationAddress
                            {
                                Street1 = "105 Chauncy St",
                                Street2 = "Suite 502",
                                State = "MA",
                                Zip = "02111",
                                ZipExt = "1767",
                                Country = "United States"
                            }
                        },
                        ResourcePrograms = new List<ResourceProgram>
                        {
                            new ResourceProgram
                            {
                                Name = "Boston Youth Flex Fund",
                                ResourceUrl = @"https://drive.google.com/file/d/1qYOKVRt0AgFz667V4_2HC-eDhTGAjC_k/view",
                                Detail = new ResourceProgramDetail
                                {
                                    Cost = "FREE",
                                    Description = new ResourceProgramDescription
                                    {
                                        Description = "Provides one-time payment to youth experiencing housing instability.",
                                    },
                                    InternalNotes = new ResourceProgramNote
                                    {
                                        InternalNotes  = "Applications must be completed by a case manager (no self-referrals)."
                                    },
                                    ProcessSteps = new ResourceProgramStep
                                    {
                                        ProcessSteps = ""
                                    },
                                    ObtainabilityRating = 1,
                                    CustomerServiceRating = 1,
                                    ProcessTime = new ResourceProcessTime
                                    {

                                    }
                                },
                                Status = new ResourceProgramStatus
                                {

                                }
                            }
                        }
                    });

                    try
                    {
                        Console.WriteLine("Before Save Changes");
                        dbContext.SaveChanges();
                        Console.WriteLine($"After Save Changes");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"save failed; msg = {ex}");
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());   
            }

        }

        static void MergeOrganization()
        {          
            using (var dbContext = new RmsDbContext())
            {
                var groupByResourceUrlQuery =
                    from resource in dbContext.ResourcePrograms
                    orderby resource.ResourceCode
                    group resource by resource.ResourceUrl into dupResource
                    where dupResource.Count() > 1
                    select new DuplicateResourceGroup(dupResource);

                foreach (var resourceGroup in groupByResourceUrlQuery)
                {
                    // skip if null
                    if (resourceGroup == null)
                        continue;
                    
                    // skip the group if not flagged for organization merging
                    if (resourceGroup.MergeOrganizations == false)
                        continue;

                    // user first element in list as the base for the merged organization
                    var mergeOrg = resourceGroup.Group.First().Org;

                    // update remaining resource records in the group to use the mergeOrg id
                    foreach (var resource in resourceGroup.Group)
                    {
                        // use other records in the group to fill in any organization missing data
                        MergeOrganization(mergeOrg, resource.Org);

                        // no need to update the resource associated with the mergeOrg record
                        if (resource.OrgId == mergeOrg.Id)
                            continue;

                        var deleteOrg = resource.Org;

                        // update resource with the mergeOrg
                        resource.OrgId = mergeOrg.Id;
                        resource.Org = mergeOrg;

                        // delete the duplicate organization and related org address
                        RmsRepository.DeleteOrganization(deleteOrg);
                    }
                }
            }
        }

        static void MergeOrganization(Organization mergeOrg, Organization groupOrg)
        {
            if (mergeOrg.Id == groupOrg.Id)
                return;

            // fill in empty fields
            if (mergeOrg.Email.IsNullOrEmpty()) { }
            
            if (mergeOrg.Fax.IsNullOrEmpty()) { }

            if (mergeOrg.Url.IsNullOrEmpty()) { }

            if (mergeOrg.Phone.IsNullOrEmpty()) { }

            // check org address fields
            if (mergeOrg.OrganizationAddresses.First().State.IsNullOrEmpty()) { }
        }
    }

    internal class DuplicateResourceGroup(IGrouping<string, ResourceProgram> dupResource)
    {
        internal IGrouping<string, ResourceProgram> Group { get; private set; } = dupResource;

        internal bool MergeOrganizations {  get; set; } = false;
        internal bool MergeContacts { get; set; } = false;
        internal bool MergeResourcePrograms{ get; set; } = false;

        internal bool RequiresDataCleaning { get; set; } = false;

        private readonly List<int> contactIdsToKeep = [];
        internal IEnumerable<int>  ContactIdsToKeep { get {  return contactIdsToKeep; } }

        public void AddContactToKeep(int contactResourceCode)
        {
            contactIdsToKeep.Add(contactResourceCode);
        }
    }
}

using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using MigrateTOUData.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Services
{
    internal class MigrateService : IMigrateService
    {
        public void Migrate()
        {
            Console.WriteLine("Start");
            try
            {
                using (var dbContext = new RmsDbContext())
                {
                    var newOrg = dbContext.Add(new Organization
                    {
                        Name = "HomeStart",
                        WebsiteUrl = @"https://www.homestart.org",
                        Email = "",
                        Phone = "6175420338",
                        Fax = "6175421454",
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
                        dbContext.SaveChanges();
                        Console.WriteLine($"Save Success!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"save failed; msg = {ex}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

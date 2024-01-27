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
            Console.WriteLine("Start Migration");
            try
            {
                using (var dbContext = new RmsDbContext())
                {
                    var newOrg = dbContext.Add(new Organization
                    {
                        Name = "South Boston Collaborative Center (The Joseph \"Dodo\")",
                        WebsiteUrl = @"https://www.southbostoncollaborativecenter.org",
                        Email = String.Empty,
                        Phone = "6175349500",
                        Fax = String.Empty,
                        OrganizationAddresses = new List<OrganizationAddress>
                        {
                            new OrganizationAddress
                            {
                                Street1 = "25 James O’Neil Street",
                                Street2 = String.Empty,
                                City = "South Boston",
                                State = "MA",
                                Zip = "02127",
                                ZipExt = String.Empty,
                                Country = "United States"
                            }
                        },
                        ResourcePrograms = new List<ResourceProgram>
                        {
                            // legacy code = 2
                            new ResourceProgram
                            {
                                Name = "South Boston Collaborative Center",
                                ResourceUrl = @"https://www.southbostoncollaborativecenter.org/services-we-offer",
                                Detail = new ResourceProgramDetail
                                {
                                    Cost = "FREE",
                                    Description = new ResourceProgramDescription
                                    {
                                        Description = "Every Wednesday, SOBO Collaborative has an open intake day between 10 am - noon on a drop-in basis.",
                                    },
                                    InternalNotes = new ResourceProgramNote
                                    {
                                        InternalNotes  = String.Empty
                                    },
                                    ProcessSteps = new ResourceProgramStep
                                    {
                                        ProcessSteps = "No prior appointments needed\r\n"+
                                        "Walk-in appointments at 1226A Columbia Road South Boston 02127\r\n"+
                                        "-Almost all insurances are accepted. Individuals can still be accepted without insurance.\r\n"+
                                        "-Bring along a proof of identification\r\n-Initial intake must be completed to participate in the services."
                                    },
                                    ObtainabilityRating = 5,
                                    CustomerServiceRating = 5,
                                    ProcessTimeId  = 0, //TODO: set to Within a week
                                },
                                Status = new ResourceProgramStatus
                                {                                      
                                    StatusTypeId = 0, // TODO: set to Expired
                                    CmsUser = "System-Migration"
                                },
                                //ResourceWithApplicationTypes -- add walk-in
                            }
                        },
                        ResourceContacts = new List<ResourceContact>
                        {
                            new ResourceContact
                            {
                                FirstName = "Patricia",
                                LastName = "Collins",
                                Phone = "6175349500"
                            }
                        }
                    }) ;

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

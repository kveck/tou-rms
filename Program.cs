using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data;
using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using System;

namespace MigrateTOUData 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            try
            {
                using (var dbContext = new VssDbContext())
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
                                ResourceId = "HOMES1",
                                Name = "Boston Youth Flex Fund",
                                ResourceUrl = @"https://drive.google.com/file/d/1qYOKVRt0AgFz667V4_2HC-eDhTGAjC_k/view",
                                Description = "Provides one-time payment to youth experiencing housing instability.",
                                Cost = "FREE",
                                Notes = "Applications must be completed by a case manager (no self-referrals).",
                                Status = "VERIFIED"
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
    }
}

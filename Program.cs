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
                    dbContext.Add(new Organization
                    {
                        Name = "Test1",
                        OrgCode = "Test01",
                        Url = "Test",
                        Email = "test@testemail.com",
                        Phone = "5035551111"
                    });

                    try
                    {
                        Console.WriteLine("Before Save Changes");
                        dbContext.SaveChanges();
                        Console.WriteLine("After Save Changes");
                    }
                    finally
                    {

                    }

                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());   
            }

        }
    }
}

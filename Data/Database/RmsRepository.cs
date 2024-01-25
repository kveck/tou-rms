using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MigrateTOUData.Data.Models;

namespace MigrateTOUData.Data.Database
{
    internal class RmsRepository()
    {
        internal static async Task DeleteOrganization (Organization org)
        {
            using (var dbContext = new RmsDbContext())
            {
                dbContext.Organizations.Remove(org);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

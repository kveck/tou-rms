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
        internal static void DeleteContact(ResourceContact contact)
        {
            throw new NotImplementedException();
        }

        internal static void DeleteOrganization (Organization org)
        {
            using (var dbContext = new RmsDbContext())
            {
                dbContext.Organizations.Remove(org);
                dbContext.SaveChanges();
            }
        }

        internal static void DeleteResource(ResourceProgram resource)
        {
            throw new NotImplementedException();
        }

        internal static void GetCityTaxonomyId(string city, string state)
        {
            using (var dbContext = new RmsDbContext())
            {
                var stateRegionQuery = dbContext
                    .RegionTaxonomies
                    .Where(a => a.Region.Equals(state) && 
                                dbContext.RegionTaxonomies
                                    .Where(b => b.Region.Equals(city) &&
                                        b.TaxonomyLeft > a.TaxonomyLeft &&
                                        b.TaxonomyLeft < a.TaxonomyRight)
                                    .Count() == 1);                        
            }
        }
    }
}

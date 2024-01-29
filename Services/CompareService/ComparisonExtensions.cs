using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Services.CompareService
{
    internal static class ComparisonExtensions
    {
        public static bool CompareResourceProgram(this ResourceProgram srcResource, ResourceProgram? compareResource)
        {
            if (compareResource == null) return false;
            if (ReferenceEquals(srcResource, compareResource)) return true;
            
            if (compareResource == null) return false;

            // compare Name field
            if (String.IsNullOrEmpty(compareResource.Name) ||
                compareResource.Name.Equals(srcResource.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                if (String.IsNullOrEmpty(compareResource.ResourceUrl) ||
                    compareResource.ResourceUrl.Equals(srcResource.ResourceUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    return srcResource.Detail.CompareResourceDetail(compareResource.Detail);
                }
            }
            
            return false;
        }

        public static bool CompareResourceDetail(this ResourceProgramDetail srcDetail, ResourceProgramDetail compareDetail)
        {
            return false;
        }

        public static bool CompareOrganization(this Organization srcOrg, Organization compareOrg)
        {
            return false;
        }
        public static bool CompareContact(this ResourceContact srcContact, ResourceContact compareContact)
        {
            return false;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using MigrateTOUData.Services.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Services.CompareService
{
    internal class CompareService
    {
        public void Compare()
        {
            using (var dbContext = new RmsDbContext()) {
                var groupByResourceUrlQuery =
                    from resource in dbContext.ResourcePrograms
                    orderby resource.ResourceCode
                    group resource by resource.ResourceUrl into dupResource
                    where dupResource.Count() > 1
                    select new DuplicateResourceGroup(dupResource);

                foreach (var dupGroup in  groupByResourceUrlQuery)
                {
                    // compare resource programs in the group
                    var firstResult = dupGroup.Group.First();
                    dupGroup.MergeResourcePrograms = dupGroup.Group.All(x => x.CompareResourceProgram(firstResult));
                    // if the resources are not equal, then data cleaning required
                    dupGroup.RequiresDataCleaning = !dupGroup.MergeResourcePrograms;

                    // compare organizations in the group
                    var groupOrgs = dupGroup.Group.Select(x=>x.Org).ToList();
                    var firstOrg = groupOrgs.First();
                    dupGroup.MergeOrganizations = groupOrgs.All(x => x.CompareOrganization(firstOrg));

                    // if the organizations are not equal, and data cleaning is not already flagged then data cleaning required
                    dupGroup.RequiresDataCleaning = dupGroup.RequiresDataCleaning && !dupGroup.MergeOrganizations;

                    // compare contacts in the group and identify which contacts to keep
                    CompareGroupContacts(groupOrgs, dupGroup);
                    dupGroup.RequiresDataCleaning &= !dupGroup.MergeContacts;

                }
            }
        }

        private void CompareGroupContacts(List<Organization> groupOrgs, DuplicateResourceGroup dupGroup)
        {
            // compare contacts in the group
            var groupContacts = groupOrgs.SelectMany(x => x.ResourceContacts).ToList();
            var firstContact = groupContacts.First();

            dupGroup.MergeContacts = true;
            // find the unique contacts, these need to be kept
            foreach (var contact in groupContacts)
            {
                // skip first contact
                if (contact.Id == firstContact.Id) continue;

                // if equal than contact can be merged, continue
                if (firstContact.CompareContact(contact)) continue;

                // contact is not equal, but is it different enough to be a different contact
                // if name is different, then different contact
                if (!firstContact.FirstName.Equals(contact.FirstName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!firstContact.LastName.Equals(contact.LastName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // make sure contact is not already in list
                        var firstKeep = dupGroup.ContactsToKeep.First();
                        var inList = dupGroup.ContactsToKeep.All(c => c.CompareContact(firstKeep));

                        // if not already in keep list then add to keep list
                        if (!inList) 
                            dupGroup.AddContactToKeep(contact);

                        continue;
                    }
                }

                dupGroup.MergeContacts = false;
            }
        }
    }
}

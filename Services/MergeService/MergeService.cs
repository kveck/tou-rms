using Azure.Core;
using Microsoft.EntityFrameworkCore;
using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using MigrateTOUData.Services.Contracts;
using MigrateTOUData.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Services.Merge
{
    internal class MergeService : IMergeService
    {
        //TODO: constructor should use DI to pass in repository class to do all db work

        /// <summary>
        /// Implementation for the merging of duplicate resources that
        /// were created by the Migration task
        /// </summary>
        void IMergeService.Merge()
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

                // merge the duplicate organizations for the resource group, if needed
                if (resourceGroup.MergeOrganizations)
                    MergeOrganization(resourceGroup);

                if (resourceGroup.MergeResourcePrograms)
                    MergeResources(resourceGroup);

                if (resourceGroup.MergeContacts)
                    MergeContacts(resourceGroup);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceGroup"></param>
        private void MergeOrganization(DuplicateResourceGroup resourceGroup)
        {
            // skip the group if not flagged for organization merging
            if (resourceGroup.MergeOrganizations == false)
                return;

            // user first element in list as the base for the merged organization
            var mergedOrg = resourceGroup.Group.First().Org;

            // update remaining resource records in the group to use the mergeOrg id
            foreach (var resource in resourceGroup.Group)
            {
                // skip the resource we are merging into
                if (resource.OrgId == mergedOrg.Id)
                    continue;

                // use other records in the group to fill in any organization missing data
                MergeOrganizationFields(mergedOrg, resource.Org);

                var deleteOrg = resource.Org;

                // update resource with the mergeOrg
                resource.OrgId = mergedOrg.Id;
                resource.Org = mergedOrg;

                // update associated resource contacts with mergeOrg
                foreach (var contact in resource.Org.ResourceContacts)
                {
                    if (contact == null) continue;

                    // set the contacts organization to the 'merged organization'                    
                    contact.OrgId = mergedOrg.Id;

                    // add the contact to the contact collection for 'merged organization' 
                    mergedOrg.ResourceContacts.Add(contact);
                }

                // delete the duplicate organization and related org address
                RmsRepository.DeleteOrganization(deleteOrg);
            }
        }
        private void MergeOrganizationFields(Organization mergeOrg, Organization sourceOrg)
        {
            if (mergeOrg.Id == sourceOrg.Id)
                return;

            // fill in empty fields
            if (string.IsNullOrEmpty(mergeOrg.Email)) { }

            if (string.IsNullOrEmpty(mergeOrg.Fax)) { }

            if (string.IsNullOrEmpty(mergeOrg.WebsiteUrl)) { }

            if (string.IsNullOrEmpty(mergeOrg.Phone)) { }

            // check org address fields
            if (string.IsNullOrEmpty(mergeOrg.OrganizationAddresses.First().State)) { }
        }


        private void MergeResources(DuplicateResourceGroup resourceGroup)
        {
            if (resourceGroup.MergeResourcePrograms == false)
                return;

            // user first element in list as the base for the merged organization
            var mergedResource = resourceGroup.Group.First();

            // update remaining resource records in the group to use the mergeOrg id
            foreach (var resource in resourceGroup.Group)
            {
                // skip the resource we are merging into
                if (resource.Id == mergedResource.Id)
                    continue;

                // use other records in the group to fill in any organization missing data
                MergeResourceFields(mergedResource, resource);

                // add resource legacy codes to merged resource legacy codes
                UpdateLegacyCodes(mergedResource, resource);

                // add resource contact to merged resource contacts
                UpdateResourceWithContacts(mergedResource, resource);

                // delete the duplicate resource and related resource tables (detail, notes, status, etc)
                RmsRepository.DeleteResource(resource);
            }
        }

        private void MergeResourceFields(ResourceProgram mergeResource, ResourceProgram sourceResource)
        {
            if (mergeResource.Id == sourceResource.Id)
                return;


        }

        private void UpdateLegacyCodes(ResourceProgram mergeResource, ResourceProgram sourceResource)
        {
            foreach (var legacyCode in sourceResource.ResourceCodeLegacies)
            {
                legacyCode.ResourceId = mergeResource.Id;
                mergeResource.ResourceCodeLegacies.Add(legacyCode);
                legacyCode.Resource = mergeResource;
            }
        }

        private void UpdateResourceWithContacts(ResourceProgram mergeResource, ResourceProgram sourceResource)
        {
            foreach (var srcContact in sourceResource.ResourceWithContacts)
            {
                srcContact.ResourceId = mergeResource.Id;
                mergeResource.ResourceWithContacts.Add(srcContact);
                srcContact.Resource = mergeResource;
            }
        }

        private void MergeContacts(DuplicateResourceGroup resourceGroup)
        {
            // skip if no contacts to merge and no contacts to kep
            if (resourceGroup.MergeContacts == false && resourceGroup.ContactsToKeep.Count == 0)
                return;

            // organization owns all the contacts, resource references the contact
            // skip if organization was not merged
            if (resourceGroup.MergeOrganizations == false)
                return;
                
            var mergedOrg = resourceGroup.Group.First().Org;
            // select the first contact that is NOT in the set of contacts to keep
            // this is the contact to use for merging
            var mergedContact = mergedOrg.ResourceContacts.First(x => !resourceGroup.ContactsToKeep.Contains(x));

            // update remaining resource records in the group to use the mergeOrg id
            foreach (var contact in mergedOrg.ResourceContacts)
            {
                // skip the resource we are merging into
                if (contact.Id == mergedContact.Id)
                    continue;

                // if contact in keep list, do not merge it with the
                if (!resourceGroup.ContactsToKeep.Contains(contact))
                {
                    // use other records in the group to fill in any contact missing data
                    MergeContactFields(mergedContact, contact);

                    // remove contact resource reference (ContactWithResources) and
                    // delete contact 
                    RmsRepository.DeleteContact(contact);
                }
            }
        }

        private void MergeContactFields(ResourceContact mergedContact, ResourceContact contact)
        {
            throw new NotImplementedException();
        }
    }
}

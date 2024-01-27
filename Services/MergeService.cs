﻿using MigrateTOUData.Data.Database;
using MigrateTOUData.Data.Models;
using MigrateTOUData.Services.Contracts;
using MigrateTOUData.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.BusinessLogic
{
    internal class MergeService : IMergeService
    {
        void IMergeService.Merge()
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

                    // merge the organizations for the resource group, if needed
                    if (resourceGroup.MergeOrganizations)
                        MergeOrganization(resourceGroup);

                    if (resourceGroup.MergeResourcePrograms)
                        MergeResources(resourceGroup);
                }

            }
        }

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

                // delete the duplicate organization and related org address
                RmsRepository.DeleteOrganization(deleteOrg);
            }
        }
        private void MergeOrganizationFields(Organization mergeOrg, Organization sourceOrg)
        {
            if (mergeOrg.Id == sourceOrg.Id)
                return;

            // fill in empty fields
            if (String.IsNullOrEmpty(mergeOrg.Email)) { }

            if (String.IsNullOrEmpty(mergeOrg.Fax)) { }

            if (String.IsNullOrEmpty(mergeOrg.WebsiteUrl)) { }

            if (String.IsNullOrEmpty(mergeOrg.Phone)) { }

            // check org address fields
            if (String.IsNullOrEmpty(mergeOrg.OrganizationAddresses.First().State)) { }
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

                // update merged resource legacy codes

                // update contact cross ref table

                // delete legacy code record for duplicate resource

                // delete the duplicate resource and related resource tables (detail, notes, status, etc)
                RmsRepository.DeleteResource(resource);
            }
        }

        private void MergeResourceFields(ResourceProgram mergeResource, ResourceProgram sourceResource)
        {
            if (mergeResource.Id == sourceResource.Id)
                return;


        }
    }
}
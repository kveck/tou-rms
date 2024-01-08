using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Data.SpreadSheetData
{
    internal class SpreadSheetTable
    {
        int Id { get; set; }
        string OrganizationName { get; set; }
        string ServiceName { get; set; }
        string WebsiteUrl { get; set; }
        string Contact { get; set; }
        string FirstNamePOC { get; set; }
        string LastNamePOC { get; set; }
        string EmailPOC { get; set; }
        string EaseToAccessResource { get; set; }
        string CustomerService { get; set; }
        string TimeTakenfromOutreachToResource { get; set; }
        string ResourceListingURL { get; set; }
        string UpdateAnyInformation { get; set; }
        string ResourceTag { get; set; }
        string EditorOrganisationName { get; set; }
        string EditorServiceName { get; set; }
        string EditorWebsiteUrl { get; set; }
        string EditorContact { get; set; }
        string EditorFirstNamePOC { get; set; }
        string EditorLastNamePOC { get; set; }
        string EditorEmailPOC { get; set; }
        string EditorEaseToAccessResource { get; set; }
        string EditorCustomerService { get; set; }
        string EditorTimeTakenfromOutreachToResource { get; set; }
        string EditorResourceListingURL { get; set; }
        string EditorUpdateAnyInformation { get; set; }
        string EditorResourceTag { get; set; }
        bool Approved { get; set; }
        bool Verified { get; set; }
        string EditedBy { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
        bool IsNationWide { get; set; }
        bool IsStateWide { get; set; }
        bool IsCityWide { get; set; }
        string Rating { get; set; }
        bool CRMOnly { get; set; }
        bool Pilot { get; set; }
        string InternalNote { get; set; }
        string UpdateAssignedTo { get; set; }
        string ReviewAssignedTo { get; set; }
        string DeclineNotes { get; set; }
        bool EditComplete { get; set; }
        bool IsDelete { get; set; }
        string ResourceAddress { get; set; }
    }
}

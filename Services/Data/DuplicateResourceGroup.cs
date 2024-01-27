using MigrateTOUData.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrateTOUData.Services.Data
{
    internal class DuplicateResourceGroup(IGrouping<string, ResourceProgram> dupResource)
    {
        internal IGrouping<string, ResourceProgram> Group { get; private set; } = dupResource;

        internal bool MergeOrganizations { get; set; } = false;
        internal bool MergeContacts { get; set; } = false;
        internal bool MergeResourcePrograms { get; set; } = false;
        internal bool RequiresDataCleaning { get; set; } = false;

        private readonly List<int> contactIdsToKeep = [];
        internal IEnumerable<int> ContactIdsToKeep { get { return contactIdsToKeep; } }

        public void AddContactToKeep(int contactResourceCode)
        {
            contactIdsToKeep.Add(contactResourceCode);
        }
    }
}

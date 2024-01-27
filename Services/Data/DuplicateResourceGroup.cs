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
        public IGrouping<string, ResourceProgram> Group { get; private set; } = dupResource;

        public bool MergeOrganizations { get; set; } = false;
        public bool MergeContacts { get; set; } = false;
        public bool MergeResourcePrograms { get; set; } = false;
        public bool RequiresDataCleaning { get; set; } = false;

        private readonly List<int> contactIdsToKeep = [];
        public ICollection<int> ContactIdsToKeep { get { return contactIdsToKeep; } }

        public void AddContactToKeep(int contactResourceCode)
        {
            contactIdsToKeep.Add(contactResourceCode);
        }
    }
}

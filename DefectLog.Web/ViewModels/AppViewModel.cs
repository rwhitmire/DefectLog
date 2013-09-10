using System.Collections.Generic;
using System.Linq;

namespace DefectLog.Web.ViewModels
{
    public class AppViewModel
    {
        public AppViewModel()
        {
            Versions = new List<VersionListItem>();
        }

        public IEnumerable<UserListItem> Users { get; set; }
        public IEnumerable<StatusListItem> Statuses { get; set; }
        public IEnumerable<VersionListItem> Versions { get; set; }
        public IEnumerable<CategoryListItem> Categories { get; set; }
        public IEnumerable<PriorityLevelItem> PriorityLevels { get; set; }
        public int CurrentUserId { get; set; }

        public int? DefaultVersion
        {
            get
            {
                if (!Versions.Any()) return null;
                return Versions.Last().Id;
            }
        }

        public int? DefaultPriorityLevel
        {
            get
            {
                if (!PriorityLevels.Any()) return null;

                var priority = PriorityLevels.SingleOrDefault(x => x.PriorityName.Contains("Med"));
                if (priority == null) return PriorityLevels.First().Id;

                return priority.Id;
            }
        }
    }
}
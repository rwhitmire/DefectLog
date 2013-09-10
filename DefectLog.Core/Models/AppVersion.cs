using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Core.Models
{
    public class AppVersion
    {
        public AppVersion()
        {
            Defects = new HashSet<Defect>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string VersionNumber { get; set; }

        public virtual ICollection<Defect> Defects { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Core.Models
{
    public class Status
    {
        public Status()
        {
            Defects = new HashSet<Defect>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string CssClass { get; set; }

        public virtual ICollection<Defect> Defects { get; set; }
    }
}
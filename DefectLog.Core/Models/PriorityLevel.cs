using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Core.Models
{
    public class PriorityLevel
    {
        public PriorityLevel()
        {
            Defects = new HashSet<Defect>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string PriorityName { get; set; }

        public virtual ICollection<Defect> Defects { get; set; }
    }
}
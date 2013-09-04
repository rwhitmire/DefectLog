using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Models
{
    public class Category
    {
        public Category()
        {
            Defects = new HashSet<Defect>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string CategoryName { get; set; }

        public virtual ICollection<Defect> Defects { get; set; }
    }
}
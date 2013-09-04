using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefectLog.Models
{
    public class Defect
    {
        public Defect()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int? DeveloperId { get; set; }
        public int? TesterId { get; set; }
        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }
        public int PriorityLevelId { get; set; }
        public int AppVersionId { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        [StringLength(10)]
        public string Build { get; set; }

        [StringLength(30)]
        public string Screen { get; set; }

        public DateTime DateLogged { get; set; }

        [ForeignKey("DeveloperId")]
        public virtual User Developer { get; set; }

        [ForeignKey("TesterId")]
        public virtual User Tester { get; set; }

        public virtual Status Status { get; set; }
        public virtual AppVersion AppVersion { get; set; }
        public virtual Category Category { get; set; }
        public virtual PriorityLevel PriorityLevel { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
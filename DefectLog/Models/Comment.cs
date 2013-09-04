using System;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DefectId { get; set; }

        [StringLength(500)]
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public virtual User User { get; set; }
        public virtual Defect Defect { get; set; }
    }
}
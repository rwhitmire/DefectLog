using System.ComponentModel.DataAnnotations;

namespace DefectLog.Areas.Admin.Forms
{
    public class AdminCategoryForm
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }
    }
}
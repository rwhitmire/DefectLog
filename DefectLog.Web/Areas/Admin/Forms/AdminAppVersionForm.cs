using System.ComponentModel.DataAnnotations;

namespace DefectLog.Web.Areas.Admin.Forms
{
    public class AdminAppVersionForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name="Version Number")]
        public string VersionNumber { get; set; }
    }
}
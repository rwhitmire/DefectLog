using System.ComponentModel.DataAnnotations;

namespace DefectLog.Areas.Admin.ViewModels
{
    public class AdminAppVersionListItem
    {
        public int Id { get; set; }

        [Display(Name = "Version Number")]
        public string VersionNumber { get; set; }

        [Display(Name = "Defect Count")]
        public int DefectCount { get; set; }
    }
}
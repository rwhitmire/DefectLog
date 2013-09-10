using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DefectLog.Web.Areas.Admin.Forms
{
    public class AdminUserForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Status")]
        public bool? IsApproved { get; set; }

        public SelectList RoleList { get; set; }

        public IEnumerable<SelectListItem> IsApprovedList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem{Text = "", Value = ""},
                    new SelectListItem{Text = "Approved", Value = "True"},
                    new SelectListItem{Text = "Denied", Value = "False"}
                };
            }
        }
    }
}
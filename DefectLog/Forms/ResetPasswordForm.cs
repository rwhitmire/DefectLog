using System;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Forms
{
    public class ResetPasswordForm
    {
        public ResetPasswordForm()
        {
        }

        public ResetPasswordForm(Guid key)
        {
            ResetPasswordKey = key;
        }

        public Guid ResetPasswordKey { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Verify Password")]
        public string VerifyPassword { get; set; }
    }
}
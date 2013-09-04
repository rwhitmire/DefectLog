using System.ComponentModel.DataAnnotations;

namespace DefectLog.Forms
{
    public class RegisterForm
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

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

        [Required]
        [StringLength(50)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Verify Password")]
        public string VerifyPassword { get; set; }
    }
}
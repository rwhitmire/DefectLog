using System.ComponentModel.DataAnnotations;

namespace DefectLog.Areas.Admin.ViewModels
{
    public class AdminUserListItem
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
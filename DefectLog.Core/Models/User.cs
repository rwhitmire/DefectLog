using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Core.Models
{
    public class User
    {
        public User()
        {
            TesterDefects = new HashSet<Defect>();
            DeveloperDefects = new HashSet<Defect>();
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string EmailAddress { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string PasswordSalt { get; set; }

        public Guid? ResetPasswordKey { get; set; }

        public bool? IsApproved { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Defect> TesterDefects { get; set; }
        public virtual ICollection<Defect> DeveloperDefects { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
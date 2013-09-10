using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectLog.Core.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
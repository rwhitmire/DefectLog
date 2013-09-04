using System.Collections.Generic;
using DefectLog.ViewModels;

namespace DefectLog.Areas.Admin.ViewModels
{
    public class AdminDefaultViewModel
    {
        public AdminDefaultViewModel()
        {
            NewUsers = new List<UserListItem>();
        }

        public IEnumerable<UserListItem> NewUsers { get; set; }
    }
}
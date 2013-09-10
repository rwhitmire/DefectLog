using System.Collections.Generic;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.Areas.Admin.ViewModels
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
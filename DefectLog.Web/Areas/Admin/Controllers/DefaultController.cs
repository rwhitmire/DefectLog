using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.ViewModels;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.Areas.Admin.Controllers
{
    public class DefaultController : AdminController
    {
        private readonly IUserService _userService;

        public DefaultController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var unapprovedUsers = _userService.NilApproved();

            var viewModel = new AdminDefaultViewModel
            {
                NewUsers = Mapper.Map<IEnumerable<UserListItem>>(unapprovedUsers)
            };

            return View(viewModel);
        }

    }
}

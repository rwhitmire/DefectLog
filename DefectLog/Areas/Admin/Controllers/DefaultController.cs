using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Areas.Admin.ViewModels;
using DefectLog.Services;
using DefectLog.ViewModels;

namespace DefectLog.Areas.Admin.Controllers
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

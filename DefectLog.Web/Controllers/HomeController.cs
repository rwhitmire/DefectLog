using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Core.Services;
using DefectLog.Web.ViewModels;

namespace DefectLog.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IStatusService _statusService;
        private readonly IVersionService _versionService;
        private readonly ICategoryService _categoryService;
        private readonly IPriorityLevelService _priorityLevelService;

        public HomeController(IUserService userService, IStatusService statusService, IVersionService versionService, 
            ICategoryService categoryService, IPriorityLevelService priorityLevelService)
        {
            _userService = userService;
            _statusService = statusService;
            _versionService = versionService;
            _categoryService = categoryService;
            _priorityLevelService = priorityLevelService;
        }

        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            var viewModel = new AppViewModel
            {
                Users = Mapper.Map<IEnumerable<UserListItem>>(_userService.GetAllForDisplay()),
                Statuses = Mapper.Map<IEnumerable<StatusListItem>>(_statusService.GetAll()),
                Versions = Mapper.Map<IEnumerable<VersionListItem>>(_versionService.GetAll()),
                Categories = Mapper.Map<IEnumerable<CategoryListItem>>(_categoryService.GetAll()),
                PriorityLevels = Mapper.Map<IEnumerable<PriorityLevelItem>>(_priorityLevelService.GetAll()),
                CurrentUserId = _userService.GetCurrent(User.Identity.Name).Id
            };

            return View(viewModel);
        }
    }
}

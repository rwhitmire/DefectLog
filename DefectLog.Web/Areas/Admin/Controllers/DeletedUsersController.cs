using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Areas.Admin.ViewModels;

namespace DefectLog.Web.Areas.Admin.Controllers
{
    public class DeletedUsersController : Controller
    {
        private readonly IUserService _userService;

        public DeletedUsersController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetAllDeleted();
            var list = Mapper.Map<IEnumerable<AdminUserListItem>>(users);
            return View(list);
        }

        public ActionResult Restore(int id)
        {
            var user = _userService.Get(id);
            var form = Mapper.Map<AdminUserForm>(user);
            return View(form);
        }

        [HttpPost, ActionName("Restore")]
        public ActionResult RestoreConfirmed(int id)
        {
            var user = _userService.Get(id);
            _userService.Restore(user);
            return RedirectToAction("Index", "Users");
        }
    }
}

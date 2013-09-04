using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Areas.Admin.Forms;
using DefectLog.Areas.Admin.ViewModels;
using DefectLog.Services;

namespace DefectLog.Areas.Admin.Controllers
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

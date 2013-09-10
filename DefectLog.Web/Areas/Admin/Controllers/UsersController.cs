using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Core.Services;
using DefectLog.Web.Areas.Admin.Forms;
using DefectLog.Web.Areas.Admin.ViewModels;

namespace DefectLog.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public ActionResult Index()
        {
            var users = _userService.GetAllForDisplay().Include(x => x.Role);
            var list = Mapper.Map<IEnumerable<AdminUserListItem>>(users);
            return View(list);
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.Get(id);
            var form = Mapper.Map<AdminUserForm>(user);

            form.RoleList = GetRoleList();

            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminUserForm form)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(form.Id);
                Mapper.Map(form, user);

                _userService.Update(user);
                return RedirectToAction("Index");
            }

            form.RoleList = GetRoleList();
            return View(form);
        }

        public ActionResult Delete(int id)
        {
            var user = _userService.Get(id);
            var form = Mapper.Map<AdminUserForm>(user);
            return View(form);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _userService.Get(id);
            _userService.Delete(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ApproveUser(int id, bool isApproved)
        {
            var user = _userService.Get(id);
            user.IsApproved = isApproved;
            _userService.Update(user);
        }

        private SelectList GetRoleList()
        {
            var roles = _roleService.GetAll();
            return new SelectList(roles, "Id", "RoleName");
        }
    }
}

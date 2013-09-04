using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using AutoMapper;
using DefectLog.Areas.Admin.Forms;
using DefectLog.Areas.Admin.ViewModels;
using DefectLog.Models;
using DefectLog.Services;
using DefectLog.Validation.Framework;

namespace DefectLog.Areas.Admin.Controllers
{
    public class VersionsController : AdminController
    {
        private readonly IAppVersionService _appVersionService;
        private readonly IValidator<AdminAppVersionForm> _appVersionValidator;

        public VersionsController(IAppVersionService appVersionService, IValidator<AdminAppVersionForm> appVersionValidator)
        {
            _appVersionService = appVersionService;
            _appVersionValidator = appVersionValidator;
        }

        public ActionResult Index()
        {
            var versions = _appVersionService.GetAll().Include(x => x.Defects);
            var items = Mapper.Map<IEnumerable<AdminAppVersionListItem>>(versions);

            return View(items);
        }

        public ActionResult Create()
        {
            var form = new AdminAppVersionForm();
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminAppVersionForm form)
        {
            _appVersionValidator.Validate(form).Populate(ModelState);

            if (ModelState.IsValid)
            {
                var appVersion = Mapper.Map<AppVersion>(form);
                _appVersionService.Insert(appVersion);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Edit(int id)
        {
            var appVersion = _appVersionService.Get(id);
            var form = Mapper.Map<AdminAppVersionForm>(appVersion);
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminAppVersionForm form)
        {
            _appVersionValidator.Validate(form).Populate(ModelState);

            if (ModelState.IsValid)
            {
                var appVersion = Mapper.Map<AppVersion>(form);
                _appVersionService.Update(appVersion);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Delete(int id)
        {
            var appVersion = _appVersionService.Get(id);
            var appVersionForm = Mapper.Map<AdminAppVersionForm>(appVersion);
            return View(appVersionForm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var appVersion = _appVersionService.Get(id);
            _appVersionService.Delete(appVersion);
            return RedirectToAction("Index");
        }
    }
}

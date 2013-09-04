using System.Web.Mvc;
using AutoMapper;
using DefectLog.Areas.Admin.Forms;
using DefectLog.Models;
using DefectLog.Services;

namespace DefectLog.Areas.Admin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public ActionResult Create()
        {
            var form = new AdminCategoryForm();
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminCategoryForm form)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(form);
                _categoryService.Insert(category);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Edit(int id)
        {
            var category = _categoryService.Get(id);
            var form = Mapper.Map<AdminCategoryForm>(category);
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminCategoryForm form)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(form);
                _categoryService.Update(category);
                return RedirectToAction("Index");
            }

            return View(form);
        }

        public ActionResult Delete(int id)
        {
            var category = _categoryService.Get(id);
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _categoryService.Get(id);
            _categoryService.Delete(category);
            return RedirectToAction("Index");
        }
    }
}

using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BigDataOrdersDbContext _context;

        public CategoryController(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        // GET: CategoryController
        public ActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return View(values);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("CategoryList");
        }

        public ActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.Find(category.CategoryId);
            
            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}
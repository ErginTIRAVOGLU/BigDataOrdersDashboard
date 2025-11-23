using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class ProductController : Controller
    {
         private readonly BigDataOrdersDbContext _context;

        public ProductController(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        // GET: ProductController
        public ActionResult ProductList(int page=1)
        {
            var pageSize=50;

            var values = _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalCount = _context.Products.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;
            return View(values);
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem("Italy", "Italy"),
                new SelectListItem("Netherlands", "Netherlands"),
                new SelectListItem("Germany", "Germany"),
                new SelectListItem("France", "France"),
                new SelectListItem("Spain", "Spain"),
                new SelectListItem("Türkiye", "Türkiye")
            };
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Countries = new List<SelectListItem>
                {
                    new SelectListItem("Italy", "Italy"),
                    new SelectListItem("Netherlands", "Netherlands"),
                    new SelectListItem("Germany", "Germany"),
                    new SelectListItem("France", "France"),
                    new SelectListItem("Spain", "Spain"),
                    new SelectListItem("Türkiye", "Türkiye")
                };
                return View(product);
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("ProductList");
        }

        public ActionResult UpdateProduct(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem("Italy", "Italy"),
                new SelectListItem("Netherlands", "Netherlands"),
                new SelectListItem("Germany", "Germany"),
                new SelectListItem("France", "France"),
                new SelectListItem("Spain", "Spain"),
                new SelectListItem("Türkiye", "Türkiye")
            };
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
          
            return View(product);
        }

 
        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Countries = new List<SelectListItem>
                {
                    new SelectListItem("Italy", "Italy"),
                    new SelectListItem("Netherlands", "Netherlands"),
                    new SelectListItem("Germany", "Germany"),
                    new SelectListItem("France", "France"),
                    new SelectListItem("Spain", "Spain"),
                    new SelectListItem("Türkiye", "Türkiye")
                };
                return View(product);
            }
            var existingProduct = _context.Products.Find(product.ProductId);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.CountryOfOrigin = product.CountryOfOrigin;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.ProductImageUrl = product.ProductImageUrl;
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }

    }
}

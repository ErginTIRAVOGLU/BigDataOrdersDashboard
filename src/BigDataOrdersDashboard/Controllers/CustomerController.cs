using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BigDataOrdersDashboard.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BigDataOrdersDbContext _context;

        public CustomerController(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        // GET: CustomerController
        public ActionResult CustomerList(int page = 1)
        {
            var pageSize = 50;

            var values = _context.Customers
                .OrderBy(p => p.CustomerId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalCount = _context.Customers.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;
            return View(values);
        }

        public ActionResult CreateCustomer()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer Customer)
        {
            if (!ModelState.IsValid)
            {

                return View(Customer);
            }
            _context.Customers.Add(Customer);
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }

        public ActionResult DeleteCustomer(int id)
        {
            var Customer = _context.Customers.Find(id);
            if (Customer != null)
            {
                _context.Customers.Remove(Customer);
                _context.SaveChanges();
            }
            return RedirectToAction("CustomerList");
        }

        public ActionResult UpdateCustomer(int id)
        {

            var Customer = _context.Customers.Find(id);
            if (Customer == null)
            {
                return NotFound();
            }

            return View(Customer);
        }


        [HttpPost]
        public ActionResult UpdateCustomer(Customer Customer)
        {
            if (!ModelState.IsValid)
            {

                return View(Customer);
            }
            var existingCustomer = _context.Customers.Find(Customer.CustomerId);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.CustomerName = Customer.CustomerName;
            existingCustomer.CustomerSurname = Customer.CustomerSurname;
            existingCustomer.CustomerEmail = Customer.CustomerEmail;
            existingCustomer.CustomerPhone = Customer.CustomerPhone;
            existingCustomer.CustomerCountry = Customer.CustomerCountry;
            existingCustomer.CustomerCity = Customer.CustomerCity;
            existingCustomer.CustomerDistrict = Customer.CustomerDistrict;
            existingCustomer.CustomerAddress = Customer.CustomerAddress;
            existingCustomer.CustomerImageUrl = Customer.CustomerImageUrl;
            _context.SaveChanges();
            return RedirectToAction("CustomerList");
        }

    }

}

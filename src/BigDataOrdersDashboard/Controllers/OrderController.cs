using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class OrderController : Controller
    {
        private readonly BigDataOrdersDbContext _context;

        public OrderController(BigDataOrdersDbContext context)
        {
            _context = context;
        }

        // GET: OrderController
        public ActionResult OrderList(int page = 1)
        {
            var pageSize = 50;

            var values = _context.Orders
                .Include(p => p.Product)
                .Include(c => c.Customer)
                .OrderBy(p => p.OrderId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalCount = _context.Orders.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.CurrentPage = page;
            return View(values);
        }

        public ActionResult CreateOrder()
        {
            ViewBag.PaymentMethods = new List<SelectListItem>
            {
                new SelectListItem("Apple Pay", "Apple Pay"),
                new SelectListItem("PayPal", "PayPal"),
                new SelectListItem("Kredi Kartı", "Kredi Kartı"),
                new SelectListItem("Banka Kartı", "Banka Kartı"),
                new SelectListItem("Nakit", "Nakit"),
                new SelectListItem("Kapıda Ödeme", "Kapıda Ödeme")
            };
            ViewBag.OrderStatuses = new List<SelectListItem>
            {
                new SelectListItem("Hazırlanıyor", "Hazırlanıyor"),
                new SelectListItem("Beklemede", "Beklemede"),
                new SelectListItem("İptal Edildi", "İptal Edildi"),
                new SelectListItem("Tamamlandı", "Tamamlandı")
            };
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                 
                return View(order);
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }

        public ActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("OrderList");
        }

        public ActionResult UpdateOrder(int id)
        {
             
            ViewBag.PaymentMethods = new List<SelectListItem>
            {
                new SelectListItem("Apple Pay", "Apple Pay"),
                new SelectListItem("PayPal", "PayPal"),
                new SelectListItem("Kredi Kartı", "Kredi Kartı"),
                new SelectListItem("Banka Kartı", "Banka Kartı"),
                new SelectListItem("Nakit", "Nakit"),
                new SelectListItem("Kapıda Ödeme", "Kapıda Ödeme")
            };
            ViewBag.OrderStatuses = new List<SelectListItem>
            {
                new SelectListItem("Hazırlanıyor", "Hazırlanıyor"),
                new SelectListItem("Beklemede", "Beklemede"),
                new SelectListItem("İptal Edildi", "İptal Edildi"),
                new SelectListItem("Tamamlandı", "Tamamlandı")
            };
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        [HttpPost]
        [HttpPost]
        public ActionResult UpdateOrder(Order order)
        {
            ViewBag.PaymentMethods = new List<SelectListItem>
            {
                new SelectListItem("Apple Pay", "Apple Pay"),
                new SelectListItem("PayPal", "PayPal"),
                new SelectListItem("Kredi Kartı", "Kredi Kartı"),
                new SelectListItem("Banka Kartı", "Banka Kartı"),
                new SelectListItem("Nakit", "Nakit"),
                new SelectListItem("Kapıda Ödeme", "Kapıda Ödeme")
            };
            ViewBag.OrderStatuses = new List<SelectListItem>
            {
                new SelectListItem("Hazırlanıyor", "Hazırlanıyor"),
                new SelectListItem("Beklemede", "Beklemede"),
                new SelectListItem("İptal Edildi", "İptal Edildi"),
                new SelectListItem("Tamamlandı", "Tamamlandı")
            };
            if (!ModelState.IsValid)
            {
                return View(order);
            }
            var existingOrder = _context.Orders.Find(order.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.ProductId = order.ProductId;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.Quantity = order.Quantity;
            existingOrder.PaymentMethod = order.PaymentMethod;
            existingOrder.OrderStatus = order.OrderStatus;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.OrderNotes = order.OrderNotes;
            
            _context.SaveChanges();
            return RedirectToAction("OrderList");
        }

    }

}

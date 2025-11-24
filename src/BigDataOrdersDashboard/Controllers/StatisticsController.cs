using BigDataOrdersDashboard.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigDataOrdersDashboard.Controllers
{
    public class StatisticsController(BigDataOrdersDbContext _context) : Controller
    {
        // GET: StatisticsController
        public ActionResult Index()
        {
            DateTime now = DateTime.Now;
            DateTime lastMonthStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime thisMonthStart = new DateTime(now.Year, now.Month, 1);

            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.ProductCount = _context.Products.Count();
            ViewBag.OrderCount = _context.Orders.Count();
            ViewBag.CustomerCount = _context.Customers.Count();
            
            ViewBag.CompletedOrderCount = _context.Orders.Where(o => o.OrderStatus == "Tamamlandı").Count();
            ViewBag.PendingOrderCount = _context.Orders.Where(o => o.OrderStatus == "Beklemede").Count(); 
            ViewBag.CancelledOrderCount = _context.Orders.Where(o => o.OrderStatus == "İptal Edildi").Count();
            ViewBag.PreparingOrderCount = _context.Orders.Where(o => o.OrderStatus == "Hazırlanıyor").Count(); 

            ViewBag.ApplePayPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Apple Pay").Count();
            ViewBag.CreditCardPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Kredi Kartı").Count();
            ViewBag.CashPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Nakit").Count();
            ViewBag.BankingPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Banka Kartı").Count();
            ViewBag.CashOnDeliveryPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Kapıda Ödeme").Count();

            ViewBag.CustomerCountry = _context.Customers.Select(c => c.CustomerCountry).Distinct().Count();
            ViewBag.CustomerCity = _context.Customers.Select(c => c.CustomerCity).Distinct().Count();

            ViewBag.LastMonthOrdersCount = _context.Orders.Where(o => o.OrderDate >= lastMonthStart).Count();
            ViewBag.ThisMonthOrdersCount = _context.Orders.Where(o => o.OrderDate >= thisMonthStart).Count();
            return View();
        }

         public ActionResult TextualStatistics()
        { 
            ViewBag.ApplePayPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Apple Pay").Count();
            ViewBag.CreditCardPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Kredi Kartı").Count();
            ViewBag.CashPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Nakit").Count();
            ViewBag.BankingPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Banka Kartı").Count();
            ViewBag.CashOnDeliveryPaymentCount = _context.Orders.Where(o => o.PaymentMethod == "Kapıda Ödeme").Count();

            var result = _context.Orders
                .AsNoTracking()
                .GroupBy(o => o.Customer.CustomerCountry)                
                .Select(g => new
                    {
                        Country = g.Key, 
                        Count = g.Count(),
                        HighPricedProduct = g.OrderByDescending(o => o.Product.UnitPrice).Select(o => o.Product.ProductName).FirstOrDefault() ?? "N/A",
                        HighPricedProductPrice = g.Max(o => o.Product.UnitPrice)
                    }
                )
                .OrderByDescending(g => g.Count)
                .FirstOrDefault();

            ViewBag.CountryWithTheMostOrders = result?.Country ?? "N/A";
            ViewBag.CountryWithTheMostOrdersCount = result?.Count ?? 0;
            ViewBag.HighPricedProductInCountry = result?.HighPricedProduct ?? "N/A";
            ViewBag.HighPricedProductPriceInCountry = result?.HighPricedProductPrice ?? 0;

            var highPricedProduct = _context.Products.OrderByDescending(p => p.UnitPrice).FirstOrDefault();
            ViewBag.HighPricedProduct = highPricedProduct?.ProductName ?? "N/A";
            ViewBag.HighPricedProductPrice = highPricedProduct?.UnitPrice ?? 0;

            var highPricedOrder = _context.Orders.Include(o => o.Product).OrderByDescending(o => o.Product.UnitPrice).FirstOrDefault();
            ViewBag.HighPricedOrderId = highPricedOrder?.OrderId ?? 0;
            ViewBag.HighPricedOrderPrice = highPricedOrder?.Product.UnitPrice ?? 0;
            // Top selling category
            var topSellingCategory = _context.Categories
                .Select(c => new {
                    CategoryName = c.CategoryName,
                    SalesCount = c.Products.Sum(p => p.Orders.Count())
                })
                .OrderByDescending(x => x.SalesCount)
                .FirstOrDefault();
            ViewBag.TopSellingCategory = topSellingCategory?.CategoryName ?? "N/A";
            ViewBag.TopSellingCategorySales = topSellingCategory?.SalesCount ?? 0;

            // Highest stock product
            var highestStockProduct = _context.Products.OrderByDescending(p => p.StockQuantity).FirstOrDefault();
            ViewBag.HighestStockProduct = highestStockProduct?.ProductName ?? "N/A";
            ViewBag.HighestStockQuantity = highestStockProduct?.StockQuantity ?? 0;

            // Most popular payment method
            var paymentMethods = new[] {
                new { Method = "Apple Pay", Count = (int)ViewBag.ApplePayPaymentCount },
                new { Method = "Kredi Kartı", Count = (int)ViewBag.CreditCardPaymentCount },
                new { Method = "Nakit", Count = (int)ViewBag.CashPaymentCount },
                new { Method = "Banka Kartı", Count = (int)ViewBag.BankingPaymentCount },
                new { Method = "Kapıda Ödeme", Count = (int)ViewBag.CashOnDeliveryPaymentCount }
            };
            var mostPopularPayment = paymentMethods.OrderByDescending(p => p.Count).FirstOrDefault();
            ViewBag.MostPopularPaymentMethod = mostPopularPayment?.Method ?? "N/A";
            ViewBag.MostPopularPaymentCount = mostPopularPayment?.Count ?? 0;

            // Most active customer
            var mostActiveCustomer = _context.Customers
                .Select(c => new {
                    CustomerName = c.CustomerName + " " + c.CustomerSurname,
                    OrderCount = c.Orders.Count()
                })
                .OrderByDescending(x => x.OrderCount)
                .FirstOrDefault();
            ViewBag.MostActiveCustomer = mostActiveCustomer?.CustomerName ?? "N/A";
            ViewBag.MostActiveCustomerOrdersCount = mostActiveCustomer?.OrderCount ?? 0;


            
            return View();
        }

    }
}

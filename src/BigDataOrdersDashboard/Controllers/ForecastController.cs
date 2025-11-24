using BigDataOrdersDashboard.Context;
using BigDataOrdersDashboard.Dtos.ForecastDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;

namespace BigDataOrdersDashboard.Controllers
{
    public class ForecastController(
        BigDataOrdersDbContext _context,
        MLContext _mlContext) : Controller
    {
        public ActionResult PaymentMethodForecast()
        {
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            // Gruplanmış veri
            var monthlyPaymentData = _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .AsEnumerable()
                .GroupBy(o => new
                {
                    Month = new DateTime(o.OrderDate.Year, o.OrderDate.Month, 1),
                    o.PaymentMethod
                })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    PaymentMethod = g.Key.PaymentMethod,
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToList();

            var forecast = new List<PaymentForecastData>();

            // Her ödeme yöntemi için tahmin
            foreach (var method in monthlyPaymentData.Select(x => x.PaymentMethod).Distinct())
            {
                var methodData = monthlyPaymentData
                    .Where(x => x.PaymentMethod == method)
                    .OrderBy(x => x.Month)
                    .Select(x => new PaymentForecastData
                    {
                        PaymentMethod = method,
                        Month = x.Month,
                        OrderCount = x.OrderCount
                    })
                    .ToList();

                // Minimum veri kontrolü
                if (methodData.Count < 8)
                    continue;

                // Otomatik windowSize
                // ML.NET şartı: trainSize > 2 * windowSize
                int trainSize = methodData.Count;
                int windowSize = Math.Max(3, trainSize / 4);        // güvenli
                int seriesLength = Math.Max(windowSize * 2, 8);     // minimum 8

                if (seriesLength > trainSize)
                    seriesLength = trainSize;

                var dataView = _mlContext.Data.LoadFromEnumerable(methodData);

                var pipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: "OrderCount",
                    windowSize: windowSize,
                    seriesLength: seriesLength,
                    trainSize: trainSize,
                    horizon: 6,
                    confidenceLevel: 0.95f,
                    confidenceLowerBoundColumn: "LowerBound",
                    confidenceUpperBoundColumn: "UpperBound"
                );

                var model = pipeline.Fit(dataView);

                var engine = model.CreateTimeSeriesEngine<PaymentForecastData, PaymentForecastPrediction>(_mlContext);

                var prediction = engine.Predict();

                var lastMonth = methodData.Last().Month;

                for (int i = 0; i < prediction.ForecastedValues.Length; i++)
                {
                    var nextMonth = lastMonth.AddMonths(i + 1);

                    forecast.Add(new PaymentForecastData
                    {
                        PaymentMethod = method,
                        Month = nextMonth,
                        OrderCount = prediction.ForecastedValues[i]
                    });
                }
            }

            return View(forecast);
        }

        public IActionResult GermanyCitiesForecast()
        {
            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2025, 12, 31);

            var germanyCityData = _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate && o.Customer.CustomerCountry == "Germany")
                .AsEnumerable()
                .GroupBy(o => new
                {
                    o.Customer.CustomerCity,
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month
                })
                .Select(g => new
                {
                    City = g.Key.CustomerCity,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    DateKey = $"{g.Key.Year}-{g.Key.Month:D2}",
                    OrderCount = g.Count()
                })
                .OrderBy(xP => xP.City)
                .ThenBy(x => x.DateKey)
                .ToList();

            var forecasts = new List<object>();

            foreach (var city in germanyCityData.Select(x => x.City).Distinct())
            {
                var cityData = germanyCityData
                    .Where(x => x.City == city)
                    .Select((x, index) => new GermanyCitiesForecastData
                    {
                        City = city,
                        MonthIndex = index + 1,
                        OrderCount = x.OrderCount
                    }).ToList();

                if (cityData.Count < 4)
                    continue;

                var dataView = _mlContext.Data.LoadFromEnumerable(cityData);

                var pipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: nameof(GermanyCitiesForecastData.OrderCount),
                    windowSize: 12,
                    seriesLength: cityData.Count,
                    trainSize: cityData.Count,
                    horizon: 12,
                    confidenceLevel: 0.95f
                    );

                var model = pipeline.Fit(dataView);
                var engine = model.CreateTimeSeriesEngine<GermanyCitiesForecastData, GermanyCitiesForecastPrediction>(_mlContext);

                var prediction = engine.Predict();

                var yearlyForecast = (int)prediction.ForecastedValues.Sum();

                var year2024Count = germanyCityData
                    .Where(x => x.City == city && x.Year == 2024)
                    .Sum(x => x.OrderCount);

                var year2025Count = germanyCityData
                    .Where(x => x.City == city && x.Year == 2025)
                    .Sum(x => x.OrderCount);

                var diff = yearlyForecast - year2025Count;
                double? growthRate = year2025Count > 0
                    ? (diff / (double)year2025Count) * 100.0
                    : (double?)null;

                forecasts.Add(new
                {
                    City = city,
                    Year2024 = year2024Count,
                    Year2025 = year2025Count,
                    Year = "2026",
                    ForecastedCount = yearlyForecast,
                    DiffTo2025 = diff,      
                    GrowthRate = growthRate    
                });
            }

            return View(forecasts);
        }

    }
}
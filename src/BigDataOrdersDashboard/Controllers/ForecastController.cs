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
        // GET: ForecastController
        public ActionResult PaymentMethodForecast()
        {
            var startDate = new DateTime(2025, 1, 1);
            var endDate = new DateTime(2025, 12, 1);

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

            foreach(var method in monthlyPaymentData.Select(x=>x.PaymentMethod).Distinct())
            {
                var methodData = monthlyPaymentData
                    .Where(x => x.PaymentMethod == method)
                    .Select((x, index) => new PaymentForecastData
                    {
                        PaymentMethod = x.PaymentMethod,
                        MonthIndex = index,
                        OrderCount = x.OrderCount
                    })
                    .ToList();

                var dataView = _mlContext.Data.LoadFromEnumerable(methodData);

                // Define the forecasting pipeline
                var pipeline = _mlContext.Forecasting.ForecastBySsa(
                    outputColumnName: "ForecastedValues",
                    inputColumnName: "OrderCount",
                    windowSize: 4,
                    seriesLength: methodData.Count,
                    trainSize: methodData.Count,
                    horizon: 3,
                    confidenceLevel: 0.95f);

                var model = pipeline.Fit(dataView);

                var forecastEngine = model.CreateTimeSeriesEngine<PaymentForecastData, PaymentForecastPrediction>(_mlContext);

                var prediction = forecastEngine.Predict();

                // 2026 yılı için tahminleri 
                for (int i = 0; i < prediction.ForecastedValues.Length; i++)
                {
                    forecast.Add(new PaymentForecastData
                    {
                        PaymentMethod = method,
                        MonthIndex = methodData.Count + i,
                        OrderCount = prediction.ForecastedValues[i]
                    });
                }
            }

            return View(forecast);
        }

    }
}

using System;

namespace BigDataOrdersDashboard.Dtos.ForecastDto;

public class PaymentForecastData
{
    public string PaymentMethod { get; set; }
    public DateTime Month { get; set; }
    public float ForecastedOrderCount { get; set; }
}

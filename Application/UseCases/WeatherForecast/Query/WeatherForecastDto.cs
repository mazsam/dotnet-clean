using VendorBoilerplate.Application.Models.Query;

namespace VendorBoilerplate.Application.UseCases.WeatherForecast
{
  public class WeatherForecastDto : PaginationDto
  {
     public WeatherForecastData[]? Data { set; get; }
  }

  public class WeatherForecastData
  {
      public DateOnly Date { get; set; }

      public int TemperatureC { get; set; }

      public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

      public string? Summary { get; set; }
  }
}
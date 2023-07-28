using MediatR;
using VendorBoilerplate.Application.Models.Query;

namespace VendorBoilerplate.Application.UseCases.WeatherForecast
{
  public class WeatherForecastQuery : PaginationQuery, IRequest<WeatherForecastDto>
  {
      public string? UserId { get; set; }
  }
}
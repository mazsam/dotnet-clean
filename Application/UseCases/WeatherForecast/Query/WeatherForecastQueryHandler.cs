using MediatR;
using System;
using VendorBoilerplate.Application.Models.Query;
using VendorBoilerplate.Application.Interfaces;
using VendorBoilerplate.Application.Misc;

namespace VendorBoilerplate.Application.UseCases.WeatherForecast
{
  public class WeatherForecastQueryHandler : IRequestHandler<WeatherForecastQuery, WeatherForecastDto>
  {
      private static readonly string[] Summaries = new[]
      {
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
      };

      public WeatherForecastQueryHandler()
      {

      }

      public async Task<WeatherForecastDto> Handle(WeatherForecastQuery request, CancellationToken cancellationToken)
      {
        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecastData
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return new WeatherForecastDto {
          Success = true,
          Message = "Success Get data Weather",
          Data = result
        };
      }
  }
}
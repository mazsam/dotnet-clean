using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VendorBoilerplate.Application.Interfaces.Authorization;
using VendorBoilerplate.Application.UseCases.WeatherForecast;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mime;

namespace VendorBoilerplate.Presenter.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("/weather")]
public class WeatherForecastController : BaseController
{
    public WeatherForecastController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<WeatherForecastDto>> GetAll()
    {
      var Query = new WeatherForecastQuery
      {
        UserId = "jhasbdjkahsgvd91121212"
      };
      return Ok(await Mediator.Send(Query));
    }
}

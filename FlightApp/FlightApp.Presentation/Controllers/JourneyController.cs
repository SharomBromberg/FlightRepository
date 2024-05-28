using FlightAPP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class FlightController : ControllerBase
{
    private IJourneyService _journeyService;

    public FlightController(IJourneyService journeyService)
    {
        _journeyService = journeyService;
    }

    [HttpGet("AllFlights")]
    public async Task<IActionResult> GetFlights()
    {
        var flights = await _journeyService.GetAllFlights();
        return Ok(flights);
    }

    [HttpGet("Flights")]
    public async Task<IActionResult> GetFlights(string origin, string destination, string currency, string type, bool allowStops)
    {
        if (type == "round")
        {
            var journeys = await _journeyService.GetRoundTripFlights(origin, destination, currency, allowStops);
            var result = journeys.Select(j => new { journey = j });
            return Ok(result);
        }
        else if (type == "oneway")
        {
            var journeys = await _journeyService.GetOneWayFlights(origin, destination, currency, allowStops);
            var result = journeys.Select(j => new { journey = j });
            return Ok(result);
        }
        else
        {
            return BadRequest("Invalid type. Must be 'round' or 'oneway'.");
        }
    }
}
using FlightAPP.Application.Interfaces;
using FlightAPP.Domain.Models;
using Newtonsoft.Json;

namespace FlightAPP.Application.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "markets.json");
        private List<Flight> _flights;
        private readonly ICurrencyService _currencyService;

        public JourneyService(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
            _flights = new List<Flight>();
            try
            {
                InitializeFlightsAsync().Wait();

            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar los vuelos.", ex);
            }

        }

        private async Task InitializeFlightsAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                _flights = JsonConvert.DeserializeObject<List<Flight>>(json) ?? new List<Flight>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer el archivo de vuelos.", ex);
            }
        }

        public async Task<IEnumerable<Flight>> GetAllFlights()
        {
            return await Task.FromResult(_flights.AsEnumerable());
        }

        private void FindRoutes(string current, string destination, HashSet<string> visited, List<List<Flight>> routes, List<Flight> route)
        {
            var flights = _flights.Where(f => f.Origin == current && !visited.Contains(f.Destination));
            foreach (var flight in flights)
            {
                route.Add(flight);
                if (flight.Destination == destination)
                {
                    routes.Add(new List<Flight>(route));
                }
                else
                {
                    visited.Add(flight.Destination);
                    FindRoutes(flight.Destination, destination, visited, routes, route);
                    visited.Remove(flight.Destination);
                }
                route.Remove(flight);
            }
        }

        public async Task<IEnumerable<Journey>> GetOneWayFlights(string origin, string destination, string currency, bool allowStops)
        {

            try
            {
                var routes = new List<List<Flight>>();
                var visited = new HashSet<string>();
                var route = new List<Flight>();
                visited.Add(origin);
                FindRoutes(origin, destination, visited, routes, route);

                if (!allowStops)
                {
                    routes = routes.Where(r => r.Count == 1 && r.First().Origin == origin && r.First().Destination == destination).ToList();
                }

                var journeys = new List<Journey>();
                foreach (var routeList in routes)
                {
                    var journey = new Journey
                    {
                        Flights = new List<Flight>(),
                        Origin = origin,
                        Destination = destination
                    };
                    foreach (var flight in routeList)
                    {
                        if (currency != "USD")
                        {
                            flight.Price = await _currencyService.ConvertCurrency("USD", currency, flight.Price);
                        }
                        journey.Flights.Add(flight);
                    }
                    journey.Price = journey.Flights.Sum(f => f.Price);
                    journeys.Add(journey);
                }

                return await Task.FromResult(journeys.AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los vuelos de ida.", ex);
            }

        }

        public async Task<IEnumerable<Journey>> GetRoundTripFlights(string origin, string destination, string currency, bool allowStops)
        {

            try
            {
                var routes = new List<List<Flight>>();
                var visited = new HashSet<string>();
                var route = new List<Flight>();

                visited.Add(origin);
                FindRoutes(origin, destination, visited, routes, route);
                var outboundFlights = routes;

                routes = new List<List<Flight>>();
                visited.Clear();
                route.Clear();

                visited.Add(destination);
                FindRoutes(destination, origin, visited, routes, route);
                var returnFlights = routes;

                var journeys = new List<Journey>();
                foreach (var outbound in outboundFlights)
                {
                    foreach (var returnFlight in returnFlights)
                    {
                        var journey = new Journey
                        {
                            Flights = new List<Flight>(),
                            Origin = origin,
                            Destination = destination
                        };
                        journey.Flights.AddRange(outbound.Select(flight => flight.Clone()));
                        journey.Flights.AddRange(returnFlight.Select(flight => flight.Clone()));
                        if (currency != "USD")
                        {
                            foreach (var flight in journey.Flights)
                            {
                                flight.Price = await _currencyService.ConvertCurrency("USD", currency, flight.Price);
                            }
                        }
                        journey.Price = journey.Flights.Sum(f => f.Price);
                        journeys.Add(journey);
                    }
                }

                if (!allowStops)
                {
                    journeys = journeys.Where(j => j.Flights.Count == 2 && j.Flights.First().Origin == origin && j.Flights.First().Destination == destination && j.Flights.Last().Origin == destination && j.Flights.Last().Destination == origin).ToList();
                }
                return journeys;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los vuelos de ida y vuelta.", ex);
            }
        }
    }
}
using FlightAPP.Domain.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace FlightAPP.Application.Services;
public class CurrencyService : ICurrencyService
{
    private readonly string _apiKey = "LnP4IErKoQcKn0bEILY3KOuQ75WMVm4p";
    private readonly string _apiUrl = "https://api.apilayer.com/exchangerates_data";
    public async Task<double> ConvertCurrency(string from, string to, double amount)
    {
        if (to.ToUpper() == "USD")
        {
            return amount;
        }

        var client = new RestClient($"{_apiUrl}/convert?to={to}&from={from}&amount={amount}");
        var request = new RestRequest();
        request.Method = Method.Get;
        request.AddHeader("apikey", _apiKey);
        request.Timeout = TimeSpan.FromMilliseconds(-1);

        var response = await client.ExecuteAsync(request);
        if (response.Content == null)
        {
            throw new Exception("La respuesta de la API de conversión de moneda está vacía.");
        }

        var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
        if (content == null || content?.result == null)
        {
            throw new Exception("No se pudo obtener el resultado de la conversión de moneda.");
        }

        return Convert.ToDouble(content?.result);
    }
    public async Task<IEnumerable<string>> GetAllCurrencies()
    {
        var client = new RestClient($"{_apiUrl}/all_currencies");
        var request = new RestRequest();
        request.Method = Method.Get;
        request.AddHeader("apikey", _apiKey);
        request.Timeout = TimeSpan.FromMilliseconds(-1);

        var response = await client.ExecuteAsync(request);
        if (response.Content == null)
        {
            throw new Exception("La respuesta de la API de monedas está vacía.");
        }

        var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
        if (content == null || content?.currencies == null)
        {
            throw new Exception("No se pudo obtener la lista de monedas.");
        }

        return content?.currencies?.ToObject<IEnumerable<string>>() ?? Enumerable.Empty<string>();
    }
}
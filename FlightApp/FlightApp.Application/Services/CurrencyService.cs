using FlightAPP.Application.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace FlightAPP.Application.Services;
public class CurrencyService : ICurrencyService
{
    private readonly string _apiKey = "CqBJVon8Nfc9hll48f15UuxICAwEChLb";
    private readonly string _apiUrl = "https://api.apilayer.com/exchangerates_data";
    public async Task<double> ConvertCurrency(string from, string to, double amount)
    {
        try
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
        catch (Exception ex)
        {
            throw new Exception("Error al convertir la moneda.", ex);

        }
    }
    public async Task<IEnumerable<string>> GetAllCurrencies()
    {
        try
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
        catch (Exception ex)
        {
            throw new Exception("Error al obtener la lista de monedas.", ex);
        }
    }
}
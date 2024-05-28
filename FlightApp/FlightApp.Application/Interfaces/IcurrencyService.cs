
namespace FlightAPP.Application.Interfaces;

public interface ICurrencyService
{
    Task<double> ConvertCurrency(string from, string to, double amount);
    Task<IEnumerable<string>> GetAllCurrencies();
}
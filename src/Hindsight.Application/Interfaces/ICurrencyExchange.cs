namespace Hindsight.Application.Interfaces;

public interface ICurrencyExchange
{
       Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency, CancellationToken cancellationToken = default);
}
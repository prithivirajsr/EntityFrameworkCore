using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreMovies.Entities.Conversions
{
    public class CurrencyToStringConverter : ValueConverter<CurrencyType, string>
    {
        public CurrencyToStringConverter() : base(
            value => MapCurrencyToString(value),
            value => MapStringToCurrencyType(value))
        {
            
        }

        private static string MapCurrencyToString(CurrencyType currencyType)
        {
            return currencyType switch
            {
                CurrencyType.Rupees => "₹",
                CurrencyType.Dollar => "$",
                CurrencyType.Euro => "€",
                _ => ""
            };
        }

        private static CurrencyType MapStringToCurrencyType(string currencyType)
        {
            return currencyType switch
            {
                "₹" => CurrencyType.Rupees,
                "$" => CurrencyType.Dollar,
                "€" => CurrencyType.Euro,
                _ => CurrencyType.UnKnown
            };
        }
    }
}

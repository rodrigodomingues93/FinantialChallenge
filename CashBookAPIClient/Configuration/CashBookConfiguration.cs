using CashBookAPIClient.Interfaces;
using CashBookAPIClient.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashBookAPIClient.Configuration
{
    public static class CashBookConfiguration
    {
        public static void AddCashBookConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<CashBookOptions>(options =>
            {
                options.Address = configuration["CashBookAPIClient:Address"];
                options.EndPoint = configuration["CashBookAPIClient:EndPoint"];
            });

            service.AddHttpClient<ICashBookClient, CashBookClient>();
        }
    }
}

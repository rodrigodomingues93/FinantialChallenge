using CashBookAPIClient.Interfaces;
using CashBookAPIClient.Options;
using CashBookDomain.DTO;
using CashBookDomain.Entities.Enums;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CashBookAPIClient
{
	public class CashBookClient : ICashBookClient
	{
		private readonly HttpClient _client;
		private readonly CashBookOptions _options;

		public CashBookClient(HttpClient client, IOptions<CashBookOptions> options)
		{
			_client = client;
			_options = options.Value;
		}

		public async Task<bool> AddCashBook(EnumOrigin origin, Guid id, string description, EnumType type, decimal value)
		{
			var endPoint = _options.GetCashBookEndPoint();

			CashBookDto cashBookDto = new CashBookDto()
			{
				Origin = origin,
                OriginId = id,
                Description = description,
                Type = type,
                Value = value
			};

			var response = await _client.PostAsJsonAsync(endPoint, cashBookDto);

			if (!response.IsSuccessStatusCode)
			{
				var error = response.Content.ToString();
				throw new Exception(error);
			}

			return response != null && response.IsSuccessStatusCode;
		}
	}
}

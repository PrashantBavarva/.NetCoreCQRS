using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Irock.POTrackingSolution.Api.Integration.Tests.Factories;

namespace Irock.POTrackingSolution.Api.Integration.Tests.Endpoints
{
    public class BaseEndpointTest
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public BaseEndpointTest(IrockPOTTrackingApiFactory IrockPOTTrackingApiFactory)
        {
            _client = IrockPOTTrackingApiFactory.CreateClient();
            _options = new JsonSerializerOptions()
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, object command)
        {
            return await _client.PostAsJsonAsync(url, command, _options);
        }
        protected async Task<TResponse?> PostAsync<TResponse>(string url, object command)
        {
            var response = await _client.PostAsJsonAsync(url, command, _options);
            return await response.Content.ReadFromJsonAsync<TResponse>(_options);
        }

    }
}

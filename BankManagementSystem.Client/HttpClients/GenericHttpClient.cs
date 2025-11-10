
using BankManagementSystem.Client.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BankManagementSystem.Client.HttpClients
{
    public class GenericHttpClient : IGenericHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public GenericHttpClient(HttpClient client, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _client = client;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _client.BaseAddress = new Uri(_configuration["ApiSetting:ClientUrl"]);
        }
        public async Task<T> DeleteAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,url);
            var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            throw new NotImplementedException();

        }

        public async Task<T> PostAsync<T>(string url, dynamic data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            request.Content = content;
            var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);

            }
            throw new NotImplementedException();
        }

        public async Task<T> PutAsync<T>(string url, dynamic data)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            var content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            request.Content = content;
            var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);

            }
            throw new NotImplementedException();
        }
    }
}

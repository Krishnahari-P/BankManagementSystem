
using BankManagementSystem.Client.Dto;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

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
            _client.BaseAddress = new Uri(_configuration["ApiSetting:ClientUrl"] ?? String.Empty);
        }

        private void AttachJwtToken()
        {
            var token = _contextAccessor.HttpContext?.User?.FindFirst("JwtToken")?.Value;
            _client.DefaultRequestHeaders.Authorization =
                string.IsNullOrEmpty(token) ? null : new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            AttachJwtToken();
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            //var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result) ?? throw new Exception("API returned null");
            }
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            AttachJwtToken();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            //var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                String result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result) ?? throw new Exception("API returned null");
            }
            throw new NotImplementedException();

        }

        public async Task<T> PostAsync<T>(string url, dynamic data)
        {
            AttachJwtToken();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
            request.Content = content;
            //var encodedData = _contextAccessor.HttpContext.User.FindFirstValue("basicauth");
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedData);
            var response = await _client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<T>(result);

            //}
            //throw new NotImplementedException();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Error: {response.StatusCode} - {body}");
            }

            return JsonConvert.DeserializeObject<T>(body) ?? throw new Exception("API returned null");

        }

        //public async Task<T> PutAsync<T>(string url, dynamic data)
        //{
        //    AttachJwtToken();
        //    var request = new HttpRequestMessage(HttpMethod.Put, url);
        //    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //    request.Content = content;
        //    var response = await _client.SendAsync(request);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<T>(result) ?? throw new Exception("API returned null");

        //    }
        //    throw new NotImplementedException();
        //}
        public async Task<T> PutAsync<T>(string url, dynamic data)
        {
            AttachJwtToken();
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            string json = JsonConvert.SerializeObject(data);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(object))
                    return default!;

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result) ?? throw new Exception("API returned null");
            }

            throw new Exception($"PUT request failed: {response.StatusCode}");
        }

    }
}

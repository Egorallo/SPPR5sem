using System.Net.Http;
using System.Text;
using System.Text.Json;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.Services.LaptopService
{
    public class ApiLaptopService : ILaptopService
    {
        private readonly HttpClient _httpClient;
        private string _pageSize;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ILogger<ApiLaptopService> _logger;
        public ApiLaptopService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiLaptopService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value!;

            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }
        public async Task<ResponseData<Laptop>> CreateLaptopAsync(Laptop laptop, IFormFile? formFile)
        {
            var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "Laptops");
            var response = await _httpClient.PostAsJsonAsync(uri, laptop, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ResponseData<Laptop>>(_serializerOptions);
                //if (formFile != null)
                //{
                //    await SaveImageAsync(data!.Data!.Id, formFile);
                //}
                return data!;
            }
            _logger.LogError($"Laptop was not created. Error: {response.StatusCode}");

            return new ResponseData<Laptop>
            {
                Success = false,
                ErrorMessage = $"Laptop was not addded. Error: {response.StatusCode}"
            };
        }

        public async Task DeleteLaptopAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress!.AbsoluteUri}Laptops/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"No data received from the server. Error: {response.StatusCode}");
            }
        }

        public async Task<ResponseData<Laptop>> GetLaptopByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Laptop/laptop-{id}");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<Laptop>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    return new ResponseData<Laptop>
                    {
                        Success = false,
                        ErrorMessage = $"Error: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"No data received from the server. Error: {response.StatusCode}");
            return new ResponseData<Laptop>()
            {
                Success = false,
                ErrorMessage = $"No data received from the server. Error: {response.StatusCode}"
            };
        }

        public async Task<ResponseData<ListModel<Laptop>>> GetLaptopListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Laptops/");

            if (categoryNormalizedName != null)
            {
                urlString.Append($"{categoryNormalizedName}/");
            };

            if (pageNo > 1)
            {
                urlString.Append($"{pageNo}");
            };

            if (!_pageSize.Equals("3"))
            {
                urlString.Append(QueryString.Create("pageSize", _pageSize.ToString()));
            }

            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Laptop>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                    return new ResponseData<ListModel<Laptop>>
                    {
                        Success = false,
                        ErrorMessage = $"Error: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"No data received from the server. Error: {response.StatusCode}");
            return new ResponseData<ListModel<Laptop>>()
            {
                Success = false,
                ErrorMessage = $"No data received from the server. Error: {response.StatusCode}"
            };
        }

        public Task UpdateLaptopAsync(int id, Laptop laptop, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}

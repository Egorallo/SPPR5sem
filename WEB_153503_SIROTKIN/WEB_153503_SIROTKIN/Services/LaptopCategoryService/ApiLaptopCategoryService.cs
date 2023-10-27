using System.Text;
using System.Text.Json;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.Services.LaptopCategoryService
{
    public class ApiLaptopCategoryService : ILaptopCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiLaptopCategoryService> _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiLaptopCategoryService(HttpClient httpClient, ILogger<ApiLaptopCategoryService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}Categories/");
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<List<Category>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}",
                    };

                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error:{ response.StatusCode}");
            return new ResponseData<List<Category>>()
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error:{ response.StatusCode }"
            };
        }
    }
}
